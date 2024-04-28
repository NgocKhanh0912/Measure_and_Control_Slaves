This project is the PC-based Control and Measurement subject project that I am studying in semester 232. The project content is as follows: the STM32 communicates with a WinForm on a computer via UART to transmit, receive data to measure and control slaves: LED, DHT11, RTC DS3231 (I2C), 8 LED 7-seg MAX7219 (SPI). 
The WinForm GUI will transmit control frame data for LED and setup frame data for the current time to RTC DS3231 through STM32. Additionally, the WinForm GUI will showcase the current LED status, temperature, humidity, and DHT11 status. The time will be displayed on 8 MAX7219 7-segment LEDs in the "HH-MM-SS" format. Below outlines the data communication process between WinForm GUI and STM32:
   
   a) The data received from the WinForm GUI comes in 2 forms: 
   - Form 1: The LED control request data: "@R2ON#" (LED On) and "@R20F#" (LED Off).
   - Form 2: The initial time setup data for RTC DS3231:
     + The time setup data received from the GUI is formatted as: "T..:..:..M". For example: "T21:30:50M" where 21 is the hour, 30 is the minute, and 50 is the second.
     + The day, date, month, year setup data received from the GUI is formatted as: "D./../../..E". For example: "D2/15/04/24" where 2 is the day of the week (Monday), 15 is the date, 04 is the month, and 24 is the year (2024).
   
   b) The data transmitted from STM32 to GUI comes in 4 forms:
   - Form 1: Respond data from LED control (software handshaking): "@R2ON#" or "@R2OF#"
   - Form 2: The temperature and humidity data (in case DHT11 is not faulty) will be transmitted in the format "T...&...H", where:
     + The data ... between 'T' and '&' represents the temperature data
     + The data ... between '&' and 'H' represents the humidity data.
   - Form 3: Error data from DHT11 will be transmitted in the format "Error: ...!", where the data ... represents the type of error.
   - Form 4: The current time obtained by STM32 via the I2C protocol from RTC DS3231:
     + The time data transmitted from STM32 to GUI is formatted as: "T..:..:..M". For example: "T20:50:30M" where 20 is the hour, 50 is the minute, and 30 is the second.
     + The day, date, month, year data transmitted from STM32 to GUI is formatted as: "D./../../20..E". For example: "D5/18/04/2024" where 5 is the day of the week (Thursday), 18 is the date, 04 is the month, and 2024 is the year.
