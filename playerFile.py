from definitions import *
import os
import logging
from loggingConfig import initLogger
from ast import literal_eval
initLogger(filePath)
class PlayerClass: # has all of the stats for the player
    def __init__(self, 
                 name="", 
                 race={},
                 health=20,
                 level=0,
                 xp=0,
                 sparePoints=0,
                 gold=0,
                 stats={"dexterity":1,
                        "agility":1,
                        "vitality":1,
                        "awareness":1,
                        "charisma":1,
                        "intelligence":1,
                        "strength":1},
                 traits = [],
                 profs = [],
                 storyLocation = [],
                 inventory = []
                 ):
        self.name = name
        self.race = race
        self.health = health
        self.level = level
        self.xp = xp
        self.sparePoints = sparePoints
        self.gold = gold
        self.stats = stats
        self.traits = traits
        self.profs = profs
        self.storyLocation = storyLocation
        self.inventory = inventory
    def __str__(self): # allows printing of a data sheet
        return f"""
        Name: {self.name}
        Health: {self.health}
        Race: {self.race['name'].capitalize()}
        Level: {self.level}
        XP: {self.xp}
        Gold: {self.gold}
        SP: {self.sparePoints}
        Dexterity: {self.stats["dexterity"]}
        Agility: {self.stats["agility"]}
        Vitality: {self.stats["vitality"]}
        Awareness: {self.stats["awareness"]}
        Charisma: {self.stats["charisma"]}
        Intelligence: {self.stats["intelligence"]}
        Strength: {self.stats["strength"]}"""
    def statAssign(self):
        stats = ["dexterity","agility","vitality","awareness","charisma","intelligence","strength"] # list of stats
        try:
            navigate = int(input(f"""
            1.Dexterity - {self.stats["strength"]}
            2.Agility - {self.stats["agility"]}
            3.Vitality - {self.stats["vitality"]}
            4.Awareness - {self.stats["awareness"]}
            5.Charisma - {self.stats["charisma"]}
            6.Intelligence - {self.stats["intelligence"]}
            7.Strength - {self.stats["strength"]}
            8.Exit
            You have {self.sparePoints} to spend.
            """)) - 1
            if navigate == 7:
                clear("i")
                print(self)
                input("Input any key to continue\n>")
                return
            elif navigate < 0:
                raise ValueError
            statUp = stats[navigate]        
        except(ValueError,IndexError): # catches unexpected values
            print("Enter a number between 1-8")
            clear("d")
            self.statAssign()
            return
        clear("i")
        while True:
            try:
                increment = int(input(f"""You have {self.sparePoints} points to spend.
        How much would you like to increase {statUp} by?\n>""")) # gets the variable for the stat that wants increasing
            except(ValueError):
                print("Enter an integer between 1-7")
                continue
            if increment > self.sparePoints:
                print(f"Not enough stat points! You have {self.sparePoints} points not {increment}")    
                self.statAssign()
                return
            else:
                newValue = increment + self.__dict__[statUp]  
                break
        while True:
            confirm = input(f"Confirm you want to increase {statUp} from {self.__dict__[statUp]} to {newValue} (y/n)\n>").lower()
            if confirm == "y":
                self.__dict__[statUp] = newValue
                self.sparePoints -= increment
                print(f"Increased {statUp} to {newValue}")
                clear("d")
                return
            elif confirm == "n":
                print("Changes aborted")
                clear("d")
                self.statAssign()
            else:
                print("Invalid Input, please enter Y or N")
                clear("d")
                return
    def assignRace(self,davacis,racesDict):
        races = ["human","elf","lizardman","dwarf"]
        raceRaw = input("""
        What race are you?
        1.Human
        2.Elf
        3.Lizardman
        4.Dwarf
        >""")
        match raceRaw:
            case "1":
                print("""
--------Human--------
-----Stat Changes----
Intelligence + 2
Starting Gold + 10
-Intrinsic Abilities-
Adaptive Mind - 15% more XP gain
----Proficiencies----
Adaptable
Tactics
Ambitous""")
            case "2":
                print("""
---------Elf---------
-----Stat Changes----
Agility + 2
Dexterity + 2
Awareness + 1
Strength - 2
-Intrinsic Abilities-
Sharp Vision - +2 to perception checks
Darkvision - Unimpeded by darkness
----Proficiencies---
Archery
Light Armour
Foraging""") 
            case "3":
                print("""
------Lizardman------
-----Stat Changes----
Base Health + 3
Dexterity + 1
Intelligence - 1
Disadvantage in Dry Biomes
-Intrinsic Abilities-
Scales - +2 defence against 'sharp' attacks
-----Proficiencies----
Spears""")            
            case "4":
                print("""
--------Dwarf---------
-----Stat Changes-----
Strength + 2
Awareness + 1
Speed - 1
-Intrinsic Abilities--
Darkvision - Unimpeded by darkness
-----Proficiencies----
Heavy Armour
Blunt""")
            case other:
                print("Unable to find race, please enter an integer between 1-4")
                clear("d")
                self.assignRace(davacis,racesDict)
                return 
        raceRaw = int(raceRaw) - 1
        self.race = racesDict[f"{races[int(raceRaw)]}"]
        self.raceDavacisLoad()
    def raceDavacisLoad(self):
        confirm = input("Are you sure you want to choose this race? (y/n)\n>").lower()
        if confirm == "y":
            davacisList = self.race["davacis"]
            for i in range(len(davacisList)):
                self.__dict__[davacis[i]] += davacisList[i]
        elif confirm == "n":
            clear("i")
            self.assignRace(davacis,racesDict)
        else:
            print("Invalid input")
            clear("d")
            self.raceDavacisLoad()
    def makeSaveSlot(self,savePath):
        saveSlot = input("What would you like to name the save slot? >").strip().replace(' ', '_')
        if "\\" in saveSlot or "/" in saveSlot:
            print("Save slot cannot contain slashes")
            clear("d")
            self.makeSaveSlot(savePath)
            return
        elif saveSlot == "":
            print("Slot name cannot be empty")
            clear("d")
            self.makeSaveSlot(savePath)
            return
        slotPath = f"{savePath}\\{saveSlot}"
        try:
            os.mkdir(f"{slotPath}")
        except(FileExistsError):
            resolved = False
            while not resolved:
                if len(os.listdir(f"{slotPath}")) == 0:
                    logging.warning("Slot creation failed due to empty slot taking name.")
                    navigate = input("Slot name is taken by an (appearingly) empty slot. Override? (y/n)\n>").lower()
                    if navigate == "y":
                        os.rmdir(slotPath)
                        resolved = True
                    elif navigate == "n":
                        print("Returning to start of slot naming.")
                        clear("d")
                    else:
                        print("Invalid Input")
            logging.warning("Slot name taken.")
            override = input("Would you like to override the slot? (y/n)\n>").lower()
            if override == "y":
                with open(f"{savePath}\\{saveSlot}\\player.txt","w"):pass
            elif override == "n":
                clear("i")
                self.makeSaveSlot(savePath)
                print("Returning to main menu")
                return
            clear("d")    
        return slotPath
    def newGame(self,savePath):
        clear("i")
        self.nameSelf()
        self.assignRace(davacis,racesDict)
        self.statAssign()
        slotPath = self.makeSaveSlot(savePath)
        self.saveGame(slotPath)
    def saveGame(self,slotPath):
        slotContent = os.listdir(f"{slotPath}")
        if "player.txt" in slotContent:
            print("Saving...")
            for attribute, value in self.__dict__.items():    
                with open(f"{slotPath}/player.txt","a") as f:
                    f.write(f"{attribute}={value}\n")
        else:
            with open(f"{slotPath}/player.txt","x") as f:
                self.saveGame(slotPath)
        clear("d")    
    def loadGame(self,savePath):
            print("What slot would you like to load")
            slots = os.listdir(savePath)
            for i in range(len(slots)): print(f"{i + 1}.{slots[i]}")
            print(f"{len(slots) + 1}.Exit")
                saveSlot = slots[sanInput(">",int,1,len(slots) + 1)] # type: ignore
                if saveSlot == len(slots) + 1:
                    return None
            slotPath = f"{savePath}/{saveSlot}"
            if len(os.listdir(f"{slotPath}")) == 0:
                logging.warning(f"{saveSlot} appears empty, this indicates a broken save.")
                delete = input("Would you like to delete the file? (y/n)\n>").lower()
                if delete == "y":
                    os.rmdir(slotPath)
                    logging.debug(f"{saveSlot} deleted")
                else:
                    clear("d")
                    self.loadGame(savePath)
                    return
            except(ValueError,IndexError):
                print("Please enter an integer listed on the left.")
                clear("d")   
                self.loadGame(savePath)
                return
            with open(f"{slotPath}/player.txt") as f:    
                lines = f.readlines()
                playerLoading = {}
                for lineNum in range(len(lines)):
                    content = lines[lineNum].split("=")
                    key = content[0];value = content[1]
                    playerLoading[key] = value
                for key in playerLoading:
                    setattr(self, key, playerLoading[key])
                self.race = dict(literal_eval(f"{self.race}"))
                while True:
                    print(self)
                    navigate = input(f"{self}\nIs this the correct file?(y/n)\n>").lower()
                    if navigate == "n":
                        print("Returning to slot selection...")
                        clear("d")
                        self.loadGame(savePath)
                        return
                    elif navigate == "y":
                        return
                    else:
                        print("Invalid input, input either 'Y' or 'N'")
    def nameSelf(self):
        self.name = input("What is your name?\n>")