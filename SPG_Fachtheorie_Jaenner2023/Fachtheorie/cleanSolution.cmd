REM @echo off
REM L�scht alle tempor�ren Visual Studio Dateien
rd /S /Q ".vs"
  
FOR /D /R %%d IN (*) DO (
  rd /S /Q "%%d\obj"
)


