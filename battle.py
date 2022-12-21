import random
import os
import json
from definitions import filePath
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
    def chooseSelf(self,spawnList,filePath,secretCond = False,diffScale = False,forceEntity = False): # secretCond does not function in this implementation
        self.rarity = random.randint(1,101)
        if forceEntity == False:
            if diffScale == True:
                self.rarity = self.rarity * diffScale
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
            if secretCond == True:
                if self.rarity == 101 and secretCond == True: # 1% and a condition must be true
                    self.rarity = "secret"
        entityToLoad = f"{spawnList[self.rarity][random.randint(0,len(spawnList[self.rarity]))]}.json"
        with open(f"{filePath}\\entities\\enemies\\{self.rarity}\\{entityToLoad}") as f: # choice needs finishing
            entityLoading = json.loads(f.read())
            for key in entityLoading:
                setattr(self, key, entityLoading[key])
enemy = enemyClass()
spawnList = {
    "common":["goblin","goblin"]}
enemy.chooseSelf(spawnList,filePath)
print(enemy)
    