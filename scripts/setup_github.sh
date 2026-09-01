#!/usr/bin/env bash
# Создаёт в текущем репозитории GitHub метки и вехи (milestones) под 6 этапов
# жизненного цикла курсовой. Требует установленный gh CLI и выполненный `gh auth login`.
set -euo pipefail

REPO="${1:-$(gh repo view --json nameWithOwner -q .nameWithOwner)}"
echo "Настройка $REPO"

declare -a STAGES=(
  "1|Выбор темы и постановка задачи|недели 1–2"
  "2|Анализ предметной области|недели 3–4"
  "3|Реализация прототипа|недели 5–10"
  "4|Интеграция и тестирование|недели 11–13"
  "5|Подготовка отчёта|недели 14–15"
  "6|Защита|недели 16–17"
)

gh label create "bug" --repo "$REPO" --color d73a4a --force >/dev/null
gh label create "experiment" --repo "$REPO" --color 0e8a16 --force >/dev/null
gh label create "docs" --repo "$REPO" --color 0075ca --force >/dev/null

for s in "${STAGES[@]}"; do
  IFS='|' read -r n title weeks <<< "$s"
  gh label create "этап-$n" --repo "$REPO" --color 5319e7 --force >/dev/null
  gh api -X POST "repos/$REPO/milestones" -f title="Этап $n. $title" -f description="$weeks" >/dev/null \
    && echo "  milestone: Этап $n. $title" || echo "  milestone Этап $n уже существует"
done
echo "Готово."
