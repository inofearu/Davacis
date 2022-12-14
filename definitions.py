import os
# map(str([list]))
import time
from loggingConfig import initLogger
filePath = os.path.dirname(os.path.realpath(__file__))
logging = initLogger(filePath)
def clear(mode):
    if mode == "d":
        time.sleep(1.25)
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
        with open("saveslocation.txt","r") as f:
            savePath = f.read()
    except(FileNotFoundError):
        savePath = f"{filePath}/saves"
        logging.warning(f"'saveslocation.txt' missing, creating at {savePath}.")
        with open("saveslocation.txt","w") as f:
            f.write(savePath)
        input("Enter any key to acknowledge >")
    if os.path.exists(savePath) == False:
        logging.warning(f"Save folder missing, creating at {savePath} ")
        os.mkdir(savePath)    
        input("Enter any key to acknowledge >")
    return savePath