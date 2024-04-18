#include "main.h"
#include <stdio.h>
#include <string.h>

#define RX_BUFFER_SIZE 128
#define DS3231_ADDR 0x68 << 1

typedef struct {
    I2C_HandleTypeDef *I2C;
    uint8_t Sec;
    uint8_t Min;
    uint8_t Hour;
    uint8_t Date;
    uint8_t Day;
    uint8_t Month;
    uint8_t Year;
    uint8_t TxTimeBuff[3];
    uint8_t RxTimeBuff[3];
    uint8_t TxDateBuff[4];
    uint8_t RxDateBuff[4];
} DS3231_Time;

// Các biến lưu dữ liệu set thời gian do người dùng nhập từ WinForm GUI
int setSec = 0;
int setMin = 0;
int setHour = 0;
int setDate = 0;
int setDay = 0;
int setMonth = 0;
int setYear = 0;

int is_Time_set = 0;
int is_Date_set = 0;

TIM_HandleTypeDef htim1;
TIM_HandleTypeDef htim2;
UART_HandleTypeDef huart1;
I2C_HandleTypeDef hi2c1;

#define DHT11_PORT GPIOA
#define DHT11_PIN GPIO_PIN_5

// Biến lưu dữ liệu 8 bit nhận được từ UART
uint8_t rx_data;

// Buffer lưu chuỗi dữ liệu nhận được từ UART
uint8_t rx_buffer[12];

// Tạo mảng char để sao chép dữ liệu từ rx_buffer, dùng để truyền vào hàm sscanf
char rx_buffer_char[12];

// Index để trỏ vào phần tử của rx_buffer
uint8_t rx_index = 0;

void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_I2C1_Init(void);
static void MX_TIM1_Init(void);
static void MX_TIM2_Init(void);
static void MX_USART1_UART_Init(void);
void Error_Handler(void);

void DS3231_Init(DS3231_Time *DS3231, I2C_HandleTypeDef *I2C_In);
void DS3231_SetTime(DS3231_Time *DS3231, uint8_t Hour, uint8_t Min, uint8_t Sec);
void DS3231_GetTime(DS3231_Time *DS3231);
void DS3231_SetDate(DS3231_Time *DS3231, uint8_t Day, uint8_t Date, uint8_t Month, uint8_t Year);
void DS3231_GetDate(DS3231_Time *DS3231);

void delay_us(uint32_t us);
static int Counting_Time(uint16_t Time_Max, int Level);
void DHT11_Init(void);
uint8_t DHT11_ReadData(uint8_t *temp, uint8_t *hum);

void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim);
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart);


int main(void)
{
    HAL_Init();

    SystemClock_Config();

    MX_GPIO_Init();

    MX_I2C1_Init();

    MX_TIM1_Init();
    MX_TIM2_Init();
    HAL_TIM_Base_Start(&htim1);

    HAL_TIM_Base_Start_IT(&htim2);

    MX_USART1_UART_Init();
    HAL_UART_Receive_IT(&huart1, &rx_data, 1);

    DS3231_Time ds3231;
    DS3231_Init(&ds3231, &hi2c1);

    while (1)
    {
		if (is_Time_set) {
			is_Time_set = 0;
			// Thiết lập giờ, phút, giây hiện tại
			DS3231_SetTime(&ds3231, setHour, setMin, setSec);
		}

		if (is_Date_set) {
			is_Date_set = 0;
			// Thiết lập thứ, ngày, tháng, năm hiện tại
			DS3231_SetDate(&ds3231, setDay, setDate, setMonth, setYear);
		}

		char buffer[50];
		DS3231_GetTime(&ds3231);
		DS3231_GetDate(&ds3231);

		// Gửi dữ liệu thứ, ngày, tháng, năm lên GUI
		sprintf(buffer, "D%01d/%02d/%02d/20%02dE", ds3231.Day, ds3231.Date, ds3231.Month, ds3231.Year);
		HAL_UART_Transmit(&huart1, (uint8_t*) buffer, strlen(buffer), HAL_MAX_DELAY);

		HAL_Delay(500);

		// Gửi dữ liệu giờ, phút, giây lên GUI
		sprintf(buffer, "T%02d:%02d:%02dM", ds3231.Hour, ds3231.Min, ds3231.Sec);
		HAL_UART_Transmit(&huart1, (uint8_t*) buffer, strlen(buffer), HAL_MAX_DELAY);

		HAL_Delay(500);
    }
}


static void I2C_Write_Time(DS3231_Time *DS3231) {
    HAL_I2C_Mem_Write(DS3231->I2C, DS3231_ADDR, 0, I2C_MEMADD_SIZE_8BIT, DS3231->TxTimeBuff, 3, 1000);
}

static void I2C_Read_Time(DS3231_Time *DS3231) {
    HAL_I2C_Mem_Read(DS3231->I2C, DS3231_ADDR, 0, I2C_MEMADD_SIZE_8BIT, DS3231->RxTimeBuff, 3, 1000);
}

static void I2C_Write_Date(DS3231_Time *DS3231) {
    HAL_I2C_Mem_Write(DS3231->I2C, DS3231_ADDR, 3, I2C_MEMADD_SIZE_8BIT, DS3231->TxDateBuff, 4, 1000);
}

static void I2C_Read_Date(DS3231_Time *DS3231) {
    HAL_I2C_Mem_Read(DS3231->I2C, DS3231_ADDR, 3, I2C_MEMADD_SIZE_8BIT, DS3231->RxDateBuff, 4, 1000);
}

static uint8_t BCD_DEC(uint8_t data) {
    return (data >> 4) * 10 + (data & 0x0F);
}

static uint8_t DEC_BCD(uint8_t data) {
    return (data / 10) << 4 | (data % 10);
}

void DS3231_Init(DS3231_Time *DS3231, I2C_HandleTypeDef *I2C_In) {
    DS3231->I2C = I2C_In;
}

void DS3231_SetTime(DS3231_Time *DS3231, uint8_t Hour, uint8_t Min, uint8_t Sec) {
    DS3231->TxTimeBuff[0] = DEC_BCD(Sec);
    DS3231->TxTimeBuff[1] = DEC_BCD(Min);
    DS3231->TxTimeBuff[2] = DEC_BCD(Hour);
    I2C_Write_Time(DS3231);
}

void DS3231_GetTime(DS3231_Time *DS3231) {
    I2C_Read_Time(DS3231);
    DS3231->Sec = BCD_DEC(DS3231->RxTimeBuff[0]);
    DS3231->Min = BCD_DEC(DS3231->RxTimeBuff[1]);
    DS3231->Hour = BCD_DEC(DS3231->RxTimeBuff[2]);
}

void DS3231_SetDate(DS3231_Time *DS3231, uint8_t Day, uint8_t Date, uint8_t Month, uint8_t Year) {
    DS3231->TxDateBuff[0] = DEC_BCD(Day);
    DS3231->TxDateBuff[1] = DEC_BCD(Date);
    DS3231->TxDateBuff[2] = DEC_BCD(Month);
    DS3231->TxDateBuff[3] = DEC_BCD(Year);
    I2C_Write_Date(DS3231);
}

void DS3231_GetDate(DS3231_Time *DS3231) {
    I2C_Read_Date(DS3231);
    DS3231->Day = BCD_DEC(DS3231->RxDateBuff[0]);
    DS3231->Date = BCD_DEC(DS3231->RxDateBuff[1]);
    DS3231->Month = BCD_DEC(DS3231->RxDateBuff[2]);
    DS3231->Year = BCD_DEC(DS3231->RxDateBuff[3]);
}


// Hàm khởi tạo chân GPIO DHT11
void DHT11_Init(void)
{
    GPIO_InitTypeDef GPIO_InitStruct = {0};

    __HAL_RCC_GPIOA_CLK_ENABLE();

    GPIO_InitStruct.Pin = DHT11_PIN;
    GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_OD;
    GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
    HAL_GPIO_Init(DHT11_PORT, &GPIO_InitStruct);

    HAL_GPIO_WritePin(DHT11_PORT, DHT11_PIN, GPIO_PIN_SET);
}


// Hàm delay x us, x được truyền vào hàm
void delay_us(uint32_t us)
{
	// Reset timer counter, mỗi lần đếm là 1 micro giây
    __HAL_TIM_SET_COUNTER(&htim1, 0);

    // Đếm số us truyền vào
    while (__HAL_TIM_GET_COUNTER(&htim1) < us)
    {
      // Đợi đếm đủ
    }
}


// Hàm đếm thời gian mà chân DHT11 đã ở mức Level (us)
static int Counting_Time(uint16_t Time_Max, int Level)
{
	int count = 0;
	while(HAL_GPIO_ReadPin(DHT11_PORT, DHT11_PIN) == Level)
	{
		if (count++ > Time_Max)
		    return HAL_TIMEOUT;
		// Mỗi lần lặp sẽ tương ứng với 1 lần delay 1 micro giây
		delay_us(1);
	}
	// Trả về số lần lặp đã xảy ra (số micro giây mà chân DHT11 đã ở mức Level)
	return count;
}


// Hàm đọc dữ liệu từ DHT11
uint8_t DHT11_ReadData(uint8_t *temp, uint8_t *hum)
{
    // Request data
    HAL_GPIO_WritePin(DHT11_PORT, DHT11_PIN, GPIO_PIN_RESET);
    // Kéo chân DHT11 xuống mức 0 trong 20ms
    delay_us(20*1000);
    HAL_GPIO_WritePin(DHT11_PORT, DHT11_PIN, GPIO_PIN_SET);
    // Kéo chân DHT11 lên mức 1 trong 40us
    delay_us(40);

    // Cấu hình lại chân DHT11 thành Input để đọc dữ liệu
    GPIO_InitTypeDef GPIO_InitStruct = {0};
    GPIO_InitStruct.Pin = DHT11_PIN;
    GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
    GPIO_InitStruct.Pull = GPIO_NOPULL;
    HAL_GPIO_Init(DHT11_PORT, &GPIO_InitStruct);

    // Đợi DHT11 phản hồi
    if (Counting_Time(80, 0) == HAL_TIMEOUT)
	    return 5;

    if (Counting_Time(80, 1) == HAL_TIMEOUT)
  	    return 6;

    // Đọc 40 bit dữ liệu
    uint8_t data[5] = {0};
    for (int i = 0; i < 40; i++)
    {
        if (Counting_Time(50, 0) == HAL_TIMEOUT)
    	    return 7;

        if (Counting_Time(70, 1) > 28)
    	    data[i/8] |= (1 << (7-(i%8)));
    }

    // Checksum để kiểm tra CRC
    if ((uint8_t)(data[0] + data[1] + data[2] + data[3]) != data[4])
        return HAL_ERROR;

    // Trả về giá trị nhiệt độ và độ ẩm
    *hum = data[0];
    *temp = data[2];

    return HAL_OK;
}


/* Hàm ngắt timer 2 mỗi khi timer 2 đếm được 2 giây
   Hàm ngắt này sẽ truyền dữ liệu nhiệt độ, độ ẩm hoặc cảnh báo lỗi Timeout/CRC

   Dữ liệu nhiệt độ và độ ẩm sẽ được truyền theo chuỗi "T...&...H", trong đó:
   Dữ liệu ... giữa 'T' và '&' là dữ liệu nhiệt độ
   Dữ liệu ... giữa '&' và 'H' là dữ liệu độ ẩm

   Dữ liệu lỗi sẽ được truyền theo chuỗi "Error: ...!", trong đó dữ liệu ... là loại lỗi
*/
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{
    if (htim->Instance == TIM2)
    {
    	uint8_t temperature = 0, humidity = 0;
    	uint8_t status = 0;
    	char buffer[50];
    	DHT11_Init();

        status = DHT11_ReadData(&temperature, &humidity);

    	if (status == HAL_OK)
    	{
    	  sprintf(buffer, "T%d&%dH", temperature, humidity);
    	  HAL_UART_Transmit(&huart1, (uint8_t *)buffer, strlen(buffer), HAL_MAX_DELAY);
    	}
    	else if (status == 5)
    	{
    	  sprintf(buffer, "Error: Respond Level 0 Timeout!");
    	  HAL_UART_Transmit(&huart1, (uint8_t *)buffer, strlen(buffer), HAL_MAX_DELAY);
    	}
    	else if (status == 6)
    	{
    	  sprintf(buffer, "Error: Respond Level 1 Timeout!");
    	  HAL_UART_Transmit(&huart1, (uint8_t *)buffer, strlen(buffer), HAL_MAX_DELAY);
    	}
    	else if (status == 7)
    	{
    	  sprintf(buffer, "Error: Data Level 0 Timeout!");
    	  HAL_UART_Transmit(&huart1, (uint8_t *)buffer, strlen(buffer), HAL_MAX_DELAY);
    	}
    	else if (status == HAL_ERROR)
    	{
    	  sprintf(buffer, "Error: CRC Error!");
    	  HAL_UART_Transmit(&huart1, (uint8_t *)buffer, strlen(buffer), HAL_MAX_DELAY);
    	}
    }
}


/* Hàm ngắt nhận UART, xử lý chuỗi dữ liệu:
1. Xử lý chuỗi dữ liệu điều khiển LED từ GUI gửi xuống và truyền ngược lại GUI (Request - Respond)
   Dữ liệu nhận được từ GUI có dạng "@R2ON#" hoặc "@R2OF#"
   Khi nhận được chuỗi "@R2ON#", bật LED ở chân A6
   Khi nhận được chuỗi "@R2OF#", tắt LED ở chân A6

2. Xử lý chuỗi dữ liệu thiết lập thời gian ban đầu cho RTC DS3231 từ GUI gửi xuống
   Dữ liệu set giờ, phút, giây nhận được từ GUI có dạng: "T..:..:..M"
   Ví dụ: "T21:30:50M" thì 21 là giờ, 30 là phút, 50 là giây

   Dữ liệu set thứ, ngày, tháng, năm nhận được từ GUI có dạng: "D./../../..E"
   Ví dụ: "D2/15/04/24" thì 2 là thứ, 15 là ngày, 04 là tháng, 24 là năm (2024)
*/
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
    if (huart->Instance == USART1)
    {
    	// Phân tích chuỗi dữ liệu điều khiển LED
		if (rx_data == '#' && rx_index == 5 && rx_buffer[0] == '@' && rx_buffer[1] == 'R' && rx_buffer[2] == '2' && rx_buffer[3] == 'O')
		{
			// Lưu kí tự '#' là kí tự kết thúc chuỗi dữ liệu
			rx_buffer[rx_index] = rx_data;

			if (rx_buffer[4] == 'N') {
				HAL_GPIO_WritePin(GPIOA, GPIO_PIN_6, GPIO_PIN_SET);
			}

			else if (rx_buffer[4] == 'F') {
				HAL_GPIO_WritePin(GPIOA, GPIO_PIN_6, GPIO_PIN_RESET);
			}

			// Response ngược lại dữ liệu nhận được lên GUI
			for (int i = 0; i < 6; i++) {
				HAL_UART_Transmit(&huart1, &rx_buffer[i], 1, HAL_MAX_DELAY);
			}
			rx_index = 0;
		}

		// Phân tích chuỗi dữ liệu set giờ, phút, giây
		else if (rx_data == 'M' && rx_buffer[0] == 'T' && rx_buffer[3] == ':' && rx_buffer[6] == ':')
		{
			is_Time_set = 1;
			// Lưu kí tự kết thúc chuỗi dữ liệu
			rx_buffer[rx_index] = rx_data;

			// Sao chép dữ liệu từ rx_buffer vào rx_buffer_char
			for (int i = 0; i < 12; i++) {
				rx_buffer_char[i] = rx_buffer[i];
			}

			// Sử dụng hàm sscanf để phân tích chuỗi dữ liệu và lấy giờ, phút và giây
			sscanf(rx_buffer_char, "T%d:%d:%dM", &setHour, &setMin, &setSec);

			rx_index = 0;
		}

		// Phân tích chuỗi dữ liệu set thứ, ngày, tháng, năm
		else if (rx_data == 'E' && rx_buffer[0] == 'D' && rx_buffer[2] == '/' && rx_buffer[5] == '/' && rx_buffer[8] == '/')
		{
			is_Date_set = 1;
			// Lưu kí tự kết thúc chuỗi dữ liệu
			rx_buffer[rx_index] = rx_data;

			// Sao chép dữ liệu từ rx_buffer vào rx_buffer_char
			for (int i = 0; i < 12; i++) {
				rx_buffer_char[i] = rx_buffer[i];
			}

			// Sử dụng hàm sscanf để phân tích chuỗi dữ liệu và lấy thứ, ngày, tháng, năm
			sscanf(rx_buffer_char, "D%d/%d/%d/%dM", &setDay, &setDate, &setMonth, &setYear);

			rx_index = 0;
		}

		else {
			// Nếu chưa gặp kí tự kết thúc thì cứ lưu dữ liệu vào mảng buffer
			rx_buffer[rx_index] = rx_data;
			rx_index++;

			if (rx_index >= 12) {
				rx_index = 0;
			}
		}

		HAL_UART_Receive_IT(&huart1, &rx_data, 1);
    }
}


void SystemClock_Config(void)
{
    RCC_OscInitTypeDef RCC_OscInitStruct = {0};
    RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

    RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSI;
    RCC_OscInitStruct.HSIState = RCC_HSI_ON;
    RCC_OscInitStruct.HSICalibrationValue = RCC_HSICALIBRATION_DEFAULT;
    RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
    RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSI_DIV2;
    RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL16;
    if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
    {
      Error_Handler();
    }

    RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK | RCC_CLOCKTYPE_SYSCLK |
                                RCC_CLOCKTYPE_PCLK1 | RCC_CLOCKTYPE_PCLK2;
    RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
    RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
    RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
    RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;
    if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK)
    {
      Error_Handler();
    }
}


static void MX_TIM1_Init(void)
{
	htim1.Instance = TIM1;
	htim1.Init.Prescaler = 63;
	htim1.Init.CounterMode = TIM_COUNTERMODE_UP;
	htim1.Init.Period = 65535;
	htim1.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	htim1.Init.RepetitionCounter = 0;
	htim1.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
	if (HAL_TIM_Base_Init(&htim1) != HAL_OK)
	{
	  Error_Handler();
	}
}


static void MX_TIM2_Init(void)
{
    htim2.Instance = TIM2;
    htim2.Init.Prescaler = 63999;
    htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
    htim2.Init.Period = 2000;
    htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
    htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
    if (HAL_TIM_Base_Init(&htim2) != HAL_OK)
    {
      Error_Handler();
    }
}


static void MX_USART1_UART_Init(void)
{
	huart1.Instance = USART1;
	huart1.Init.BaudRate = 9600;
	huart1.Init.WordLength = UART_WORDLENGTH_8B;
	huart1.Init.StopBits = UART_STOPBITS_1;
	huart1.Init.Parity = UART_PARITY_NONE;
	huart1.Init.Mode = UART_MODE_TX_RX;
	huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
	huart1.Init.OverSampling = UART_OVERSAMPLING_16;
	if (HAL_UART_Init(&huart1) != HAL_OK)
	{
	  Error_Handler();
	}
}


static void MX_I2C1_Init(void) {
    hi2c1.Instance = I2C1;
    hi2c1.Init.ClockSpeed = 100000;
    hi2c1.Init.DutyCycle = I2C_DUTYCYCLE_2;
    hi2c1.Init.OwnAddress1 = 0;
    hi2c1.Init.AddressingMode = I2C_ADDRESSINGMODE_7BIT;
    hi2c1.Init.DualAddressMode = I2C_DUALADDRESS_DISABLE;
    hi2c1.Init.OwnAddress2 = 0;
    hi2c1.Init.GeneralCallMode = I2C_GENERALCALL_DISABLE;
    hi2c1.Init.NoStretchMode = I2C_NOSTRETCH_DISABLE;
    if (HAL_I2C_Init(&hi2c1) != HAL_OK) {
        Error_Handler();
    }
}


static void MX_GPIO_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStruct = {0};

	__HAL_RCC_GPIOD_CLK_ENABLE();
	__HAL_RCC_GPIOA_CLK_ENABLE();

	HAL_GPIO_WritePin(GPIOA, GPIO_PIN_6, GPIO_PIN_RESET);

	GPIO_InitStruct.Pin = GPIO_PIN_6;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
	HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);
}


void Error_Handler(void)
{
    __disable_irq();
    while (1)
    {
    }
}
