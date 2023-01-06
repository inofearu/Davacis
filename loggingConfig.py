def initLogger(filePath):
    import logging
    from datetime import datetime
    # ----------------------------------- Time ----------------------------------- #
    timeNow = str(datetime.now()).replace(" ","_").replace(":","-")
    timeNow = timeNow[0:16]
    # ------------------------------ Logger Creation ----------------------------- #
    logger = logging.getLogger(__name__)
    logging.basicConfig(level=logging.DEBUG,format='%(asctime)s - %(levelname)s:%(message)s')
    fileLogger = logging.FileHandler(f'{filePath}\\logs\\{timeNow}.log')
    fileLogger.setLevel(logging.DEBUG)
    consoleLogger = logging.StreamHandler()
    consoleLogger.setLevel(logging.ERROR)
    # ---------------------------- Formatter Creation ---------------------------- #
    simpleFormatter = logging.Formatter("%(levelname)s] %(name)s: %(message)s")
    verboseFormatter = logging.Formatter("%(asctime)s] %(levelname)s [%(filename)s %(name)s %(funcName)s (%(lineno)d)]: %(message)s")
    fileLogger.setFormatter(verboseFormatter)
    consoleLogger.setFormatter(simpleFormatter)
    # ------------------------------ Handler Adding ------------------------------ #
    logger.addHandler(consoleLogger)
    logger.addHandler(fileLogger)
    logging.debug("Logging initalised!")
    return logging