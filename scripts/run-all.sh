#!/usr/bin/env bash
# Builds every example, runs it, and checks that it wrote at least one output file.
# Usage: scripts/run-all.sh [extra dotnet build arguments, e.g. -p:UseLocalVDocs=true]
set -euo pipefail

root="$(cd "$(dirname "$0")/.." && pwd)"
cd "$root"

dotnet build VDocs.Examples.slnx --nologo -v q "$@"

failed=()
for project in $(find . -name '*.csproj' -not -path './scripts/*' | sort); do
  dir="$(dirname "$project")"
  name="${dir#./}"
  rm -rf "$dir/output"

  if (cd "$dir" && dotnet run --no-build "$@" > run.log 2>&1) && [ -n "$(ls -A "$dir/output" 2>/dev/null)" ]; then
    echo "PASS  $name"
  else
    echo "FAIL  $name"
    sed 's/^/      /' "$dir/run.log"
    failed+=("$name")
  fi
  rm -f "$dir/run.log"
done

if [ ${#failed[@]} -gt 0 ]; then
  echo
  echo "${#failed[@]} example(s) failed: ${failed[*]}"
  exit 1
fi
echo
echo "All examples passed."
