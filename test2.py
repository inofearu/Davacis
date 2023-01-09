import curses
import sys
def main(argv):
  # -------------------------------- init phase -------------------------------- #
  stdscr = curses.initscr()
  curses.echo() # Do not echo keys back to the client.
  curses.curs_set(False) # Turn off blinking cursor
  if curses.has_colors():
    curses.start_color()
  # -------------------------------- end of init ------------------------------- #
    # Coordinates start from top left, in the format of y, x.
    stdscr.addstr(0, 0, "Hello, world!")
    screenDetailText = "This screen is [" + str(curses.LINES) + "] high and [" + str(curses.COLS) + "] across."
    startingXPos = int ( (curses.COLS - len(screenDetailText))/2 )
    stdscr.addstr(3, startingXPos, screenDetailText)
    stdscr.addstr(5, curses.COLS - len("Press a key to quit."), "Press a key to quit.")

    # Actually draws the text above to the positions specified.
    stdscr.refresh()

    # Grabs a value from the keyboard without Enter having to be pressed (see cbreak above)
    stdscr.getch()
if __name__ == "__main__":
  main(sys.argv[1:])