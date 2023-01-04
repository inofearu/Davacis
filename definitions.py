import os
from time import sleep
from loggingConfig import initLogger
filePath = os.path.dirname(os.path.realpath(__file__))
logging = initLogger(filePath)
def clear(mode): # clear function
    if mode == "d":
        sleep(1.25)
    os.system('cls')
racesDict = {
    "human":{
        "name":"human",
        "davacis":(0,0,0,0,0,2,0),
        "traits":("Adaptive Mind"),
        "profs":("Adaptable","Tactics","Ambitous"),
        "biomeDrawback":("")},
    "elf":{
        "name":"elf",
        "davacis":(2,2,0,1,0,0,-2),
        "traits":("Sharp Vision","Darkvision"),
        "profs":("Archery","Light Armour","Foraging"),
        "biomeDrawback":("")},
    "lizardman":{
        "name":"lizardman",
        "davacis":(1,0,0,0,0,-1,0),
        "traits":("Sturdy"),
        "profs":("Spear Mastery"),
        "biomeDrawback":("Dry")},
    "dwarf":{
        "name":"dwarf",
        "davacis":(0,0,0,1,0,0,2),
        "traits":("Darkvision","Slow"),
        "profs":("Heavy Armour","Blunt"),
        "biomeDrawback":("Water")}}
davacis = ["dexterity","agility","vitality","awareness","charisma","intelligence","strength"]
def defineSavePath(filePath):
    try:
        with open("saveslocation.txt","r") as f: # reads the text file to get user defined save location
            savePath = f.read()
    except(FileNotFoundError): # fallback creates save folder in working directory
        savePath = f"{filePath}/saves"
        logging.warning(f"'saveslocation.txt' missing, creating at {savePath}.")
        with open("saveslocation.txt","w") as f: # writes fallback to savelocation
            f.write(savePath)
        input("Enter any key to acknowledge >")
    if os.path.exists(savePath) == False:
        logging.warning(f"Save folder missing, creating at {savePath} ")
        os.mkdir(savePath)    
        input("Enter any key to acknowledge >")
    return savePath
def sanInput(message,desiredType=None,valMin=None,valMax=None,vals=[],clearOnLoop=False):
    while True:
        if clearOnLoop:clear("d")
        userInput = input(message)
        if desiredType != None:
                try:
                    userInput = desiredType(userInput)
                except ValueError:
                    match desiredType.__name__:
                        case "int":
                            hrData = "an integer"
                        case "str":
                            hrData = "a character"
                        case "float":
                            hrData = "a number" 
                        case "bool":
                            hrData = "'1' or '0'"
                        case other:
                            raise SyntaxError(f"{desiredType} is not a handled type")
                    print(f"Invalid data, please enter {hrData}.")
                    continue
        if valMin != None:
            if isinstance(userInput,int) or isinstance(userInput,float):
                if valMin > userInput: 
                    print(f"Please enter a value greater than {valMin}.")
                    continue
            elif isinstance(userInput,str):
                if valMin > len(userInput):
                    print(f"Please enter a value longer than {valMin} characters.")
                    continue
        if valMax != None:
            if isinstance(userInput,int) or isinstance(userInput,float):
                if valMax < userInput: 
                    print(f"Please enter a value less than {valMax}.")
                    continue
            elif isinstance(userInput,str):
                if valMax < len(userInput):
                    print(f"Please enter a value shorter than {valMax} characters.")
                    continue
        if vals != []:
            if userInput not in vals:
                print("Please enter one of the listed values: (\"{0}\")".format())
        return userInput