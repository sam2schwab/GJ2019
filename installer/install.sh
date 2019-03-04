#version=$(<ProjectSettings/ProjectVersion.txt);
#version=${version#*: };
#echo "Downloading Unity version $version"

mkdir /c/Temp;
echo "Downloading Unity version 2018.3.6f1";
curl https://netstorage.unity3d.com/unity/a220877bc173/Windows64EditorInstaller/UnitySetup64-2018.3.6f1.exe --output /c/Temp/UnitySetup.exe;
echo "Installing Unity";
cat /c/Temp/UnitySetup.exe # -S -D=/c/Temp/Unity;