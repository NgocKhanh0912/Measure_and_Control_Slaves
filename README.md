Update: Currently, I haven't finished integrating the code snippet of RTC DS3231 into the main program, building the WinForm GUI and data processing for RTC DS3231.

This project is the PC-based Control and Measurement subject project that I am studying in semester 232. The project content is as follows:

The STM32 communicates with a WinForm on a computer via UART to transmit, receive data to measure and control slaves: LED, DHT11, RTC DS3231. The WinForm will send control frame data for LED and setup frame data for current time to RTC DS3231 down to STM32.
   
   a) The data received from the WinForm GUI is the LED control request data: "@R2ON#" (LED On) and "@R20F#" (LED Off).
   
   b) The data transmitted from STM32 to GUI comes in 3 forms:
   - Form 1: respond data from LED control (software handshaking): "@R2ON#" or "@R2OF#"
   - Form 2: The temperature and humidity data (in case DHT11 is not faulty) will be transmitted in the format "T...&...H", where:
     + The data ... between 'T' and '&' represents the temperature data
     + The data ... between '&' and 'H' represents the humidity data.
   - Form 3: Error data from DHT11 will be transmitted in the format "Error: ...!", where the data ... represents the type of error.


