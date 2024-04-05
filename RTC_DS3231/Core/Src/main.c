#include "main.h"
#include <stdio.h>
#include <string.h>

#define DS3231_ADDR 0x68<<1

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

I2C_HandleTypeDef hi2c1;

UART_HandleTypeDef huart1;

void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_I2C1_Init(void);
static void MX_USART1_UART_Init(void);

void DS3231_Init(DS3231_Time *DS3231, I2C_HandleTypeDef *I2C_In);
void DS3231_SetTime(DS3231_Time *DS3231, uint8_t Hour, uint8_t Min, uint8_t Sec);
void DS3231_GetTime(DS3231_Time *DS3231);
void DS3231_SetDate(DS3231_Time *DS3231, uint8_t Day, uint8_t Date, uint8_t Month, uint8_t Year);
void DS3231_GetDate(DS3231_Time *DS3231);

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

int main(void) {
    HAL_Init();
    SystemClock_Config();
    MX_GPIO_Init();
    MX_I2C1_Init();
    MX_USART1_UART_Init();

    DS3231_Time ds3231;
    DS3231_Init(&ds3231, &hi2c1);

    // Thiết lập thời gian và ngày tháng ban đầu
    DS3231_SetTime(&ds3231, 21, 58, 0); // 21:58:00
    DS3231_SetDate(&ds3231, 2, 25, 3, 24); // Thứ Hai, ngày 25 tháng 3 năm 2024

    while (1) {
        char buffer[50];
        DS3231_GetTime(&ds3231);
        DS3231_GetDate(&ds3231);

        // Hiển thị dữ liệu ngày và giờ
        sprintf(buffer, "Ngay: %02d/%02d/%04d \n", ds3231.Date, ds3231.Month, ds3231.Year);
        HAL_UART_Transmit(&huart1, (uint8_t *)buffer, strlen(buffer), HAL_MAX_DELAY);

        sprintf(buffer, "Gio: %02d:%02d:%02d \n", ds3231.Hour, ds3231.Min, ds3231.Sec);
        HAL_UART_Transmit(&huart1, (uint8_t *)buffer, strlen(buffer), HAL_MAX_DELAY);

        HAL_Delay(2000); // Chờ 2 giây
    }
}

void SystemClock_Config(void) {
    RCC_OscInitTypeDef RCC_OscInitStruct = {0};
    RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

    /** Initializes the RCC Oscillators according to the specified parameters
    * in the RCC_OscInitTypeDef structure.
    */
    RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSI;
    RCC_OscInitStruct.HSIState = RCC_HSI_ON;
    RCC_OscInitStruct.HSICalibrationValue = RCC_HSICALIBRATION_DEFAULT;
    RCC_OscInitStruct.PLL.PLLState = RCC_PLL_NONE;
    if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK) {
        Error_Handler();
    }

    /** Initializes the CPU, AHB and APB buses clocks
    */
    RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK | RCC_CLOCKTYPE_SYSCLK | RCC_CLOCKTYPE_PCLK1 | RCC_CLOCKTYPE_PCLK2;
    RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_HSI;
    RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
    RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV1;
    RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

    if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_0) != HAL_OK) {
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

static void MX_USART1_UART_Init(void) {
    huart1.Instance = USART1;
    huart1.Init.BaudRate = 9600;
    huart1.Init.WordLength = UART_WORDLENGTH_8B;
    huart1.Init.StopBits = UART_STOPBITS_1;
    huart1.Init.Parity = UART_PARITY_NONE;
    huart1.Init.Mode = UART_MODE_TX_RX;
    huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
    huart1.Init.OverSampling = UART_OVERSAMPLING_16;
    if (HAL_UART_Init(&huart1) != HAL_OK) {
        Error_Handler();
    }
}

static void MX_GPIO_Init(void) {
    __HAL_RCC_GPIOA_CLK_ENABLE();
    __HAL_RCC_GPIOB_CLK_ENABLE();
}

void Error_Handler(void) {
    __disable_irq();
    while (1) {
    }
}


