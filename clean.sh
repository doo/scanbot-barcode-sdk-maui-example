set -e

# Below script will, manual clean, because the regular clean doesn't remove
# the intermediate project files created by 'nuget restore'

# For All projects except Single Libraries
  for folder in */
  do
    rm -rf "$folder"/obj "$folder/bin"
    echo $folder
  done

  # For visual studio project cache
  rm -rf ./.vs