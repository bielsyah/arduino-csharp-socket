#include <ESP8266WiFi.h>

int port = 5050;
WiFiServer server(port);

const char* ssid = "Your WiFi";
const char* pass = "Your WiFi Password";


void setup() 
{
  Serial.begin(9600);
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, pass);

  Serial.println("Connecting to WiFi...");
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }

  Serial.println("Connected!");
  Serial.print("IP Address: ");
  Serial.println(WiFi.localIP());
  server.begin();
}

void loop() 
{
  WiFiClient client = server.available();
  
  if (client) 
  {
    if(client.connected())
    {
      Serial.println("Client Connected");
    }
    
    while(client.connected())
    {      
      while(client.available()>0)
      {
        Serial.write(client.read());
      }
      while(Serial.available()>0)
      {
        client.write(Serial.read());
      }
    }
    client.stop();
    Serial.println("Client disconnected");    
  }
}
