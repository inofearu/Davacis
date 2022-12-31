from definitions import clear
from definitions import davacis 
from definitions import filePath
from loggingConfig import initLogger
initLogger(filePath)
def openItemDesigner(filePath):
    weapon = {}
    itemPath = f"{filePath}\\entities\\items"
    itemName = input("What do you want to call the item?\n>")
    weapon.update(damage = chooseWeaponDamage(chooseItemType()))
    weapon.update(rarity = chooseItemRarity())
    weapon.update(statReqs = chooseStatReqs(davacis))
    weapon.update(value = chooseWeaponValue())
    weapon.update(allowTraits = chooseTraitAllow())    
    weapon.update(isUnique = chooseWeaponUnique())
    print(weapon)
def chooseItemType():
    types = ["weapons","armour","misc"]
    try:
        type = types[int(input("""
        What type of item do you want to make?
            1. Weapon
            2. Armour
            3. Misc\n>""")) - 1]
    except(TypeError,IndexError):
        print("Invalid Input")
        clear("d")
        chooseItemType()
        return
    return type
def chooseWeaponTypes():
    Finished = False
    while not Finished:
        try:
            weaponType = int(input("""
            What damage types should the weapon do?
            1. Crush
            2. Slash
            3. Pierce
            4. Magic
            5. Projectile
            6. Etherial
            7. Go Back
            """))
            if weaponType > 7 or weaponType < 1:
                raise IndexError
        except(TypeError,IndexError):
            print("Invalid Input")
            clear("d")
            chooseWeaponTypes()
            return
        if weaponType == "7":
            Finished = True
            break
        finish = input("Do you want to add more damage types?\n1. Yes\n 2. No\n>")
        if finish == "2":
            Finished = True    
            break
        else:
            pass
def chooseWeaponDamage(weaponType):
    types = ["crush","slash","pierce","magic","projectile","etherial"]
    Valid = False
    while not Valid:
        try:
            weaponDamageVals = [int(input(f"What is the least {types[weaponType].capitalize()} damage should the weapon do?\n>")), int(input(f"What is the most {types[weaponType].capitalize()} damage should the weapon do?\n>"))]
            Valid = True
        except(TypeError):
            print("Invalid Input")
            clear("d")
            chooseWeaponDamage(weaponType)
            return
        weaponDamageDict = {}   
        for i in range(len(weaponType)):
            weaponDamageDict[types[weaponType]] = weaponDamageVals
        return weaponDamageDict
def chooseItemRarity():
    raritys = ["common","uncommon","rare","epic","legendary","mythical","unobtainable"]
    try:
        itemRarity = raritys[int(input("""Choose a rarity:
        1. Common
        2. Uncommon
        3. Rare
        4. Epic
        5. Legendary
        6. Mythical
        7. Unobtainable
        >""")) - 1]
        return itemRarity    
    except(TypeError,IndexError):
        print("Invalid Input")
        clear("d")
        chooseItemRarity()

def chooseStatReqs(davacis):
    statCat = [];statCatDict = {};statsReq = set()
    try:
        statsReq.add(int(input("""
        What stat categories are needed? (input multiple numbers if needed)?
        1. Dexterity
        2. Agility
        3. Vitality
        4. Awareness
        5. Charisma
        6. Intelligence
        7. Strength""")))
        for i in range(len(statsReq)):statCat.append(davacis[statsReq])
    except(TypeError,IndexError):
        print("Invalid Input")
        clear("d")
        chooseStatReqs(davacis)
        return
    for i in range(len(statCat)):
        try:
            statCatNum = int(input(f"How high should the player's {statCat[i]} be?"))  
            statCatDict[statCat[i]] = statCatNum

        except(TypeError):
            print("Invalid Input")
            clear("d")
            chooseStatReqs(davacis)
            return
    return statCatDict
def chooseWeaponValue():
    try:
        weaponValue = int(input("What should the item be worth?\n>"))
    except(TypeError):
        print("Invalid Input")
        clear("d")
        chooseWeaponValue()
        return
    return weaponValue
def chooseTraitAllow():
    try:
        allowTraits = int(input("Should the weapon be allowed to have traits?\n1. Yes\n2. No"))
        allowTraits = bool(allowTraits)
    except(TypeError):
        print("Invalid Input")
        clear("d")
        chooseTraitAllow()
        return
    return allowTraits
def chooseWeaponUnique():
    try:
        isUnique = int(input("Is the weapon unique (excluded from standardloot)?\n1. Yes\n2. No"))
        isUnique = bool(isUnique)
    except(TypeError):
        print("Invalid Input")
        clear("d")
        chooseWeaponUnique()
        return
