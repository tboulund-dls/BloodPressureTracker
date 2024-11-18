#pragma once
#include <iostream>
#include <cstring>

namespace bms
{
  /// @brief Patient model, used to store the data into the database
  struct patientModel
  {
    std::string ssn;
    std::string mail;
    std::string name;
  };
  /// @brief Measurement model, used to store the data into the databse
  struct measurementModel
  {
    unsigned int id;
    std::string ssn;
    std::string date;

    unsigned int systolic;
    unsigned int diastolic;
    unsigned char seen;
  };
  struct queryModel
  {
    const char* quarystring;
    const char* table;
    const char* value;
  };




  #define char_map_size 10
  /*
    * Json element, are created to Read Json files, from the web_server.
      The plan is to create something that can read the request and parse
      it to the database. 

      This way i can simulate the flow of the application without, creating the
      web-server. and still keep it realistic. 



      Christian .j
    * 
   */
  class JsonElement{
    public:
    JsonElement(const char* data){
      json = data;
    }

    JsonElement(){
      json = nullptr;
    } 

    void LoadString(const char* data){
      json = data;
    }

    struct Attribute
    {
      int grp_id;       //- Indent
      int grp_element;  //- Element
      char key[255];    //- Id key for Element
      char value[255];
    };


    Attribute *find(std::string key){
      for (size_t i = 0; i < attributeSize; i++)
      {
        if(key[0] == pAttributes[i].key[0])
        {
          bool isFound = true;
          for (size_t ix = 0; ix < key.size(); ix++)
          {
            if(key[ix] == '\0' || pAttributes[i].key[ix] == '\0')
              break;

            if(key[ix] != pAttributes[i].key[ix]){
              isFound = false;
              break;
            }
          }

          if(isFound){
            return &pAttributes[i];
          }
        } 
      }
      return nullptr;
    }





    void handleRawData(){
      attributeSize = seekLength();
      pAttributes = new Attribute[attributeSize];

      int x1 = 0;
      int x2 = 0;
      int z  = 0;
      int z1 = 0;

      int attribute     = 1;
      int elementGroup  = 0;
      int keyPos        = 0;

      while(json[x1] != '\0'){

        const char item = json[x1];
        if(item == '{'){
          attribute = 0;
          z++;
        }
        if(item == '}'){
          z--;
        }

        if(item == ' ' || item == '\t' || item == '\r'){
          x1++;
          continue;
        }



        //- < Start Attribute or group >
        if(item == '"'){
          x1++;
          x2 = x1 + 1;

          //- Move to the end of the Key & value
          while (true)
          {
            const char i = json[x2];
            if(i == '\n' || i == ',' || i == '"')
              break;
            
            if(i == '{'){
              elementGroup = 1;
            }

            if(i == '}')
            {
              elementGroup = 0;
            }

            x2++;
          }


          //- Extract the Key or value from the json.
          if(x2 - x1> 0)
          {
            std::string output;
            output.assign(json + x1, x2 - x1);

            if(attribute){ //- Attribute is 1
              //- [value] Store the value of the attribute
              
              strncpy(pAttributes[keyPos].value, output.c_str(), sizeof(pAttributes[keyPos].value) - 1);
              pAttributes[keyPos].value[sizeof(pAttributes[keyPos].value) - 1] = '\0';

              //- Storing the values    
              pAttributes[keyPos].grp_id = z;
              pAttributes[keyPos].grp_element = z1;

              keyPos++; //- Move to next Attribute in array!
              attribute = 0;
            } else {
              //- [key] Store the key value used as Attribute id
              strncpy(pAttributes[keyPos].key, output.c_str(), sizeof(pAttributes[keyPos].key) - 1);
              pAttributes[keyPos].key[sizeof(pAttributes[keyPos].key) - 1] = '\0';
              attribute = 1;
            }
          }
          x1 = x2;
        }
        x1++;
      }


      printf("\n\n");
      for (size_t i = 0; i < attributeSize; i++)
      {
        std::cout << "Key: " << pAttributes[i].key << "\tvalue: " << pAttributes[i].value <<"\r\t\t\t\t\t\tZ:" << pAttributes[i].grp_id <<"\tEID: " << pAttributes[i].grp_element <<  "\n";
      }
      printf("\nDone!\n");
    }



    int seekLength()
    {
      int length    = 0;
      int position  = 0;
      int state     = 0;


      while(json[position] != '\0'){
        switch (json[position])
        {
        case '"':
          if(state == 0){
            length++;
            state = 1;
          }
          break;

        case '\n':
          state = 0;
          break;
        
        default:
          break;
        }

        position++; //- move to next char
      }
      return length;
    }
    


    Attribute *pAttributes;
    int attributeSize = 0;
    const char *json;
  };
}
