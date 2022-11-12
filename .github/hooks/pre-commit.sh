#!/bin/sh
#
# File Version: $Id$
# Usage:
# Remove the .sh file extension and the leading space when you put the script in your hooks folder!
#
# Purpose: 
# Add this script as a pre-commit hook to your git repository to prevent
# accidentally committing files which do not end in a new line.
# 
# Source: http://eng.wealthfront.com/2011/03/corrective-action-with-gits-pre-commit.html
# Version: 2011-03-08
# Related: http://stackoverflow.com/questions/13223868/how-to-stage-line-by-line-in-git-gui-although-no-newline-at-end-of-file-warnin

# From pre-commit.sample hook
against=HEAD

# Files (not deleted) in the index
files=$(git diff-index --cached --name-only --diff-filter=d "$against")
result=0  # track exit code
if [ -n "$files" ]; then
  for f in $files; do
    # Only report known text files...
    if [ -z "$(git diff-index --cached --stat=1 "$against" "$f" | grep -is '| Bin')" ]; then
      # Only match regular files (e.g. no symlinks)
      # See: https://stackoverflow.com/a/8347325
      if [ "$(git ls-files --stage "$f" | awk '{print $1}' | head -c2 - )" = "10" ]; then
        # Using staged version of file instead of working dir
        # See: http://stackoverflow.com/a/5032436/5343341
        if [ -n "$(git cat-file blob "$(git ls-files --stage "$f" | awk '{print $2}')" | tail -c1 - )" ]; then
          # Report error
          if [ "$result" -lt "1" ]; then
            echo "Error: The following files have no trailing newline:" 1>&2
            echo '  (use "git commit --no-verify" to ignore this message)' 1>&2
          fi
          echo -en "\t" 1>&2
          echo "$f" 1>&2
          result=1
        fi
      fi
    fi
  done
fi

exit $result