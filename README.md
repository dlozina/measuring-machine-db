# measuring-machine-db

*** Under Dev ***

APP Images:

Manual input of CNC machine correction:
![Manual](https://user-images.githubusercontent.com/28594128/57850050-3f994d80-77dd-11e9-944e-596777c557fd.PNG)

Goal of this Windows APP is to gather measurment results from machine (let's call it MS) and store results in MySQL databse. 
After storing results we need to process results and send them to two other machines (M1 and M1)

I have divided this APP into couple of projects:

1. Communication interface with MS (We have PLC device for this machine)
2. Business logic for processing (We are calculating tool offset)
3. Comunication interface for other two machines (They need same type of CNC interface)
4. GUI for APP
