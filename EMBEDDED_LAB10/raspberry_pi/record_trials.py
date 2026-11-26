"""Lab 10 Part D: enter observed on/off states from 20 actual trials."""
import csv
import statistics
from pathlib import Path

rows = []
for trial in range(1, 21):
    condition = 'present' if trial <= 10 else 'absent'
    expected = 'on' if trial <= 10 else 'off'
    print(f'Trial {trial}: red {condition}; expected LED {expected}')
    while True:
        observed = input('Observed LED (on/off): ').strip().lower()
        if observed in ('on', 'off'):
            break
    while True:
        answer = input('Measured physical latency in ms (blank if not measured): ').strip()
        try:
            delay = '' if not answer else float(answer)
            if delay != '' and delay < 0:
                raise ValueError
            break
        except ValueError:
            print('Enter a nonnegative number or leave blank.')
    rows.append(dict(trial=trial, red_object=condition, expected_led=expected,
                     observed_led=observed, correct=observed == expected,
                     latency_ms=delay))

out = Path(__file__).resolve().parents[1] / 'results' / 'lab10_trials.csv'
out.parent.mkdir(exist_ok=True)
with out.open('w', newline='', encoding='utf-8') as file:
    writer = csv.DictWriter(file, fieldnames=rows[0].keys())
    writer.writeheader()
    writer.writerows(rows)
correct = sum(row['correct'] for row in rows)
latencies = [row['latency_ms'] for row in rows if row['latency_ms'] != '']
print(f'Accuracy: {correct}/20 = {correct / 20 * 100:.1f}%')
if latencies:
    print(f'Mean measured latency: {statistics.mean(latencies):.2f} ms')
else:
    print('Latency not measured.')
print('Saved:', out)
