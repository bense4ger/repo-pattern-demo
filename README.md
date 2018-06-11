# Repository Pattern Demo
This is a quick and dirty example of what the repository pattern looks like in practice (in .Net, at least).  Feel free to clone and play around.

## Prerequisites
None, really.  The app is written in .Net core (C# 7.2) so you'll need that to build and run it but that's about it. There shouldn't be any surprises.

## Data
There are two Data Contexts available and both read from files (quicker and dirtier than setting up, for example, EF).  If you want to read data and see it displayed you'll need to create the appropriate files in your home directory. e.g `Game.json` and `Game.csv`.

## Notes
This is not production code!  

There are several things which would be different in a "real world" app (e.g. project dependencies) and several shortcuts have been taken (e.g. mapper config).

## Questions, Comments, Reviews etc
If you've got a question, comment or would like some clairification the feel free to open an issue.  Likewise, if you want to make some changes/improvements then feel free to submit a pull request :)
