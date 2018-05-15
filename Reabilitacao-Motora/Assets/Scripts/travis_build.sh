#! /bin/sh
TEXTOAMARELO="\033[01;33m"
TEXTOVERDE="\033[01;32m"
NORMAL="\033[m"


project="Reabilitacao-Motora"

echo "${TEXTOAMARELO}Entering the $project folder to start build script  ${NORMAL}"
cd Reabilitacao-Motora
echo "${TEXTOVERDE}========================================================================================================${NORMAL}"

echo "${TEXTOAMARELO}Current folder contains: ${NORMAL}"
ls
echo "${TEXTOVERDE}========================================================================================================${NORMAL}"

echo "${TEXTOAMARELO}Attempting to build $project for OSX  ${NORMAL}"
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -logFile /dev/stdout -projectPath $(pwd) -buildOSXUniversalPlayer "Build/osx/$project.app" -quit
echo "${TEXTOVERDE}========================================================================================================${NORMAL}"

echo "${TEXTOAMARELO}Attempting to build $project for Linux  ${NORMAL}"
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -logFile /dev/stdout -projectPath $(pwd) -buildLinuxUniversalPlayer "Build/linux/$project.exe" -quit
echo "${TEXTOVERDE}========================================================================================================${NORMAL}"

echo "${TEXTOAMARELO}Attempting to build $project for Windows  ${NORMAL}"
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -logFile /dev/stdout -projectPath $(pwd) -buildWindowsPlayer "Build/windows/$project.exe" -quit
echo "${TEXTOVERDE}========================================================================================================${NORMAL}"

echo "${TEXTOAMARELO}Attempting to zip builds ${NORMAL}"
zip -r Build/linux.zip Build/linux/
zip -r Build/mac.zip Build/osx/
zip -r Build/windows.zip Build/windows/
