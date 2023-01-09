import random
import json
import os
from definitions import filePath
from loggingConfig import initLogger
initLogger(filePath)
import logging
class enemyClass:
    def __init__(self):
        self.name = ""
        self.health = 0
        self.level = 0
        self.dexterity = 1
        self.agility = 1
        self.vitality = 1
        self.awareness = 1
        self.charisma = 1
        self.intelligence = 1   
        self.strength = 1
        self.traits = [""]
        self.profs = [""]
        self.drops = [""]
    def __str__(self):
        return f"""
        Name: {self.name}
        Health: {self.health}
        Level: {self.level}
        Dexterity: {self.dexterity}
        Agility: {self.agility}
        Vitality: {self.vitality}
        Awareness: {self.awareness}
        Charisma: {self.charisma}
        Intelligence: {self.intelligence}
        Strength: {self.strength}
        Traits: {self.traits}
        Proficiencies: {self.profs}
        Drop List: {self.drops}"""
    def spawnSelf(self,spawnList):
        self.rarity = random.randint(1,100)
        if self.rarity < 58: # 58%
            self.rarity = "common"
        elif self.rarity < 83: # 25%
            self.rarity = "uncommon"
        elif self.rarity < 93: # 10%
            self.rarity = "epic"  
        elif self.rarity < 98: # 5%
            self.rarity = "legendary"
        elif self.rarity < 100: # 2%
            self.rarity = "mythical"
        entityToLoad = f"{spawnList[self.rarity][random.randint(0,len(spawnList[self.rarity]) - 1)]}.json"
        try:
            with open(f"{os.getcwd()}\\entities\\enemies\\{self.rarity}\\{entityToLoad}") as f:
                self.__dict__.update(json.loads(f.read()))
        except(FileNotFoundError):
            logging.error("Failed to find entity file.W")
            print("This indicates deleted or renamed entity files.")
            input("Enter anything to acknowledge >")
        print(entityToLoad)