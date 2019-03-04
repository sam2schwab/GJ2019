VERSION="$(git describe --exact-match --tags $(git log -n1 --pretty='%h'))"
DIR="$( cygpath -w $( cd "$( dirname "${BASH_SOURCE[0]}" )/.." >/dev/null 2>&1 && pwd ))"
echo "Building Home World $VERSION..."
#./build.bat $DIR >/dev/null;
echo "Build complete"
echo "Creating installer..."
echo "#define MyAppVersion \"$VERSION\"" | cat - create_installer.iss > temp.iss
iscc ./temp.isc /O-
echo "Installer creation complete"
explorer "$DIR/Builds"