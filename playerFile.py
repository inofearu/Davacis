from definitions import *
import os
from ast import literal_eval
import logging
from loggingConfig import initLogger
initLogger(filePath)
class PlayerClass: # has all of the stats for the player
    def __init__(self):
        self.name = ""
        self.race = {}
        self.health = 0
        self.level = 0
        self.xp = 0
        self.sparePoints = 0
        self.gold = 0
        self.davacis = {"dexterity":0,
                        "agility":0,
                        "vitality":0,
                        "awareness":0,
                        "charisma":0,
                        "intelligence":0,
                        "strength":0}
        self.traits = []
        self.profs = []
        self.storyLocation = []
        self.inventory = []
        self.equipped = {
            "armour":{
                "Head":"",
                "Torso":"",
                "Arms":"",
                "Legs":"",
                "Feet":"",
                "Hands":""},
            "weapons":{
                "Left Hand":"",
                "Right Hand":"",
                "Back":""
                },
            "equipment":{
                "Mana Reactor":"",
                "Necklace":"",
                "Rings":["",""],
                "Trinket":["",""]    
            }}
    def __str__(self): # allows printing of a data sheet
        return f"""
        Name: {self.name}
        Health: {self.health}
        Race: {self.race['name'].capitalize()}
        Level: {self.level}
        XP: {self.xp}
        Gold: {self.gold}
        SP: {self.sparePoints}
        Dexterity: {self.davacis["dexterity"]}
        Agility: {self.davacis["agility"]}
        Vitality: {self.davacis["vitality"]}
        Awareness: {self.davacis["awareness"]}
        Charisma: {self.davacis["charisma"]}
        Intelligence: {self.davacis["intelligence"]}
        Strength: {self.davacis["strength"]}"""
    def statAssign(self):
        stats = ["dexterity","agility","vitality","awareness","charisma","intelligence","strength"] # list of davacis
        try:
            navigate = int(input(f"""
            1.Dexterity - {self.davacis["strength"]}
            2.Agility - {self.davacis["agility"]}
            3.Vitality - {self.davacis["vitality"]}
            4.Awareness - {self.davacis["awareness"]}
            5.Charisma - {self.davacis["charisma"]}
            6.Intelligence - {self.davacis["intelligence"]}
            7.Strength - {self.davacis["strength"]}
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
                newValue = increment + self.__dict__[statUp] # increments the stat  
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
        raceRaw = int(raceRaw) - 1 # corrects the input and 0 list start difference
        self.race = racesDict[f"{races[int(raceRaw)]}"] # sets player race to the actual statistics for the race from the dict
        self.raceDavacisLoad() # takes the races stats and assigns them to the players
    def raceDavacisLoad(self):
        confirm = input("Are you sure you want to choose this race? (y/n)\n>").lower()
        if confirm == "y":
            davacisList = self.race["davacis"]
            for i in range(len(davacisList)):
                self.davacis[davacis[i]] += davacisList[i]
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
        self.name = input("What is your name?\n>")
        self.assignRace(davacis,racesDict)
        self.statAssign()
        slotPath = self.makeSaveSlot(savePath)
        self.saveGame(slotPath)
    def saveGame(self,slotPath):
        slotContent = os.listdir(f"{slotPath}")
        if "player.txt" in slotContent:
            print("Saving...")
            for attribute, value in self.__dict__.items():    # replace with jsons
                with open(f"{slotPath}/player.txt","a") as f:
                    f.write(f"{attribute}={value}\n")
        else:
            with open(f"{slotPath}/player.txt","x") as f:
                self.saveGame(slotPath)
        clear("d")    
    def loadGame(self,savePath):
            loaded = False
            print("What slot would you like to load")
            slots = os.listdir(savePath)
            for i in range(len(list(slots))): print(f"{i + 1}.{slots[i]}")
            exitNum = len(slots) + 1
            print(f"{exitNum}.Exit")
            slotSelect = sanInput(">",int,1,len(slots) + 1)
            if slotSelect == exitNum: return loaded
            else: slotSelect -= 1  
            saveSlot = slots[int(slotSelect)]
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
            with open(f"{slotPath}/player.txt") as f:    
                lines = f.readlines()
                playerLoading = {}
                for lineNum in range(len(lines)):
                    content = lines[lineNum].split("=")
                    key = content[0];value = content[1]
                    playerLoading[key] = value
                for key in playerLoading: # replace with jsons
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
                        loaded = True
                        return loaded
                    else:
                        print("Invalid input, input either 'Y' or 'N'") 
    def startBattle(self,enemyCount,enemyClass,spawnList,preGenEnemies = []):
        enemyInstances = []
        battleInProgress = True
        if len(preGenEnemies) < enemyCount:
            for i in range(enemyCount):
                enemyInstances.append(enemyClass())
                enemyInstances[i].spawnSelf(spawnList)
        elif len(preGenEnemies) > enemyCount:
            raise SyntaxError("Pregenerated enemy count cannot be greater than enemy count.")
        while battleInProgress == True: # battle loop
            for i in range(len(enemyInstances)):
                print(f"""
    TEMP UI:
        ENEMY ID: {i}
        NAME: {enemyInstances[i].name}
        HEALTH: {enemyInstances[i].health}""")
            action = sanInput("""
        PLAYER:
            HEALTH: {self.health}
            MANA: {}
            ACTIONS:
                1. FIGHT
                2. SKILL
                3. ITEM
                4. FLEE""",int,1,4) - 1
            if action == 1:
                if len(enemyInstances) != 1:
                    print("Which enemy would you like to attack?")
                    for i in range(len(enemyInstances)):
                        print(f"{i + 1}.{enemyInstances[i].name}")
                target = enemyInstances[sanInput(">",int,1,len(enemyInstances) + 1) - 1]
                
            elif action == 2:
                pass
            elif action == 3:
                pass
            elif action == 4:
                pass