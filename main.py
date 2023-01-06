################################################################################
# Boujh Dawizard
# Imports
# Files
# Modules
import os
from definitions import * # definitions will not cause naming conflcits
import logging
from loggingConfig import initLogger
logging = initLogger(filePath)
######################################
from playerFile import PlayerClass
savePath = defineSavePath(filePath)
from itemMaker import openItemDesigner
################################################################################
# Main Menu
clear("i")
def mainMenu(PlayerClass):
    clear("d")
    correct = 0
    while correct != 1:
        navigate = input(f"""
    -------Main Menu-------
        1.New Game
        2.Load Game
        3.Help
        4.Settings
        5.Quit
        >""")
        match navigate:
            case "1":
                player = PlayerClass()
                player.newGame(savePath)
            case "2":
                player = PlayerClass()
                loaded = player.loadGame(savePath)
                if not loaded:
                    mainMenu(PlayerClass)
                    return
                else:
                    pass #game call here
            case "3":
                print("To Be Added")
                mainMenu(PlayerClass)
            case "4":
                navigate = input("""
                ---------Settings---------
                1. Wipe Saves Folder
                2. Open Item Designer
                3. Exit
                >""")          
                if navigate == "1":
                    confirm = ("Confirm you want ALL saves deleted. (y/n) \n>")
                    if confirm == 1:
                        folders = os.listdir(f"{filePath}/saves")
                        for folder in folders:
                            os.remove(folder)
                    else:
                        mainMenu(PlayerClass)
                        return
                if navigate == "2":
                    openItemDesigner(filePath)
                else:
                    mainMenu(PlayerClass)
                    return
            case "5":
                confirm = input("Are you sure you want to exit? (y/n)").lower()
                if confirm == "y":
                    raise SystemExit
                elif confirm == "n":
                    print("Returning to main menu...")
                else:
                    print("Invalid input, returning to start of creation.")
                mainMenu(PlayerClass)
            case other:
                print("Incorrect value input. Please enter a number from 1-5.")     
                mainMenu(PlayerClass)
        correct = 1
mainMenu(PlayerClass)