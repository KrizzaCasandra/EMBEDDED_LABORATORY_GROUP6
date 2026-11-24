import socket
import time
import statistics


PI_IP = "127.0.0.1"
PORT = 5000

latencies = []

try:
    with socket.create_connection(
        (PI_IP, PORT),
        timeout=10
    ) as client:

        print("Connected to Raspberry Pi")

        with client.makefile("r", encoding="utf-8") as stream:
            for i in range(20):
                line = stream.readline()

                if not line:
                    print("Server disconnected")
                    break

                try:
                    sent, light, motion = line.strip().split(",")

                    latency_ms = (
                        time.time() - float(sent)
                    ) * 1000

                except ValueError:
                    continue

                latencies.append(latency_ms)

                print(
                    f"Light={light}, "
                    f"Motion={motion}, "
                    f"Latency={latency_ms:.2f} ms"
                )

except (OSError, socket.timeout) as error:
    print("Connection error:", error)

if latencies:
    print("\n--- Network Results ---")
    print("Samples received:", len(latencies))
    print(
        f"Mean latency: "
        f"{statistics.mean(latencies):.2f} ms"
    )
    print(
        f"Minimum latency: "
        f"{min(latencies):.2f} ms"
    )
    print(
        f"Maximum latency: "
        f"{max(latencies):.2f} ms"
    )
else:
    print("No valid sensor readings received.")
