import random
import os
import json
from definitions import filePath, spawnList
import logging
from loggingConfig import initLogger
initLogger(filePath)
def spawnEnemies(genCount):
    enemyList = []
    for i in range(genCount - 1):
        enemyList.append(enemyClass())
    return enemyList
class enemyClass:
    def __init__(self):
        self.name = ""
        self.health = 20
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
    def spawnSelf(self,spawnList,filePath,rareScale,secretCond = False,forceEntity = False):
        self.rarity = random.randint(1,100)
        if forceEntity == False:
            self.rarity = self.rarity * rareScale
            if secretCond == True:
                self.rarity = "secret"
            elif self.rarity < 58: # 58%
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
            with open(f"{filePath}\\entities\\enemies\\{self.rarity}\\{entityToLoad}") as f:
                self.__dict__.update(json.loads(f.read()))
        except(FileNotFoundError):
            logging.error("Failed to find entity file.")
            print("This indicates deleted or renamed entity files.")
            input("Enter anything to acknowledge >")
    def startBattle(self,enemyCount):
        spawnEnemies(enemyCount) #  making this a function may not be needed
        
# --------------------------------- Temp Code -------------------------------- #
enemyList = spawnEnemies(3)
print(enemyList)