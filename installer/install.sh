VERSION="$(git describe --exact-match --tags $(git log -n1 --pretty='%h'))"
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )/.." >/dev/null 2>&1 && pwd )"
DIRWIN="$( cygpath -w $DIR)"
pushd "$DIR/installer"
echo "Building Home World $VERSION..."
./build.bat $DIRWIN >/dev/null;
echo "Build complete"
echo "Creating installer..."
echo "#define MyAppVersion \"$VERSION\"" | cat - create_installer.iss > temp.iss
iscc ./temp.iss
rm -f ./temp.iss
popd
echo "Installer creation complete"
explorer "$DIRWIN\\Builds"