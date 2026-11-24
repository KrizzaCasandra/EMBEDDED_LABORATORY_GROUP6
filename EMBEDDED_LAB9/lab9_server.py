import socket
import serial
import time

SERIAL_PORT = (
    "/dev/serial/by-id/"
    "usb-Arduino_Srl_Arduino_Uno_75439333235351B040E0-if00"
)

BAUD_RATE = 9600
HOST = "0.0.0.0"
PORT = 5000

ser = serial.Serial(SERIAL_PORT, BAUD_RATE, timeout=1)
time.sleep(2)

try:
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server:
        server.setsockopt(
            socket.SOL_SOCKET,
            socket.SO_REUSEADDR,
            1
        )

        server.bind((HOST, PORT))
        server.listen(1)

        print("Waiting for client on port", PORT)

        while True:
            conn, addr = server.accept()
            print("Connected:", addr)

            try:
                with conn:
                    while True:
                        line = ser.readline().decode(
                            "utf-8",
                            errors="ignore"
                        ).strip()

                        parts = line.split(",")

                        if len(parts) != 2:
                            continue

                        try:
                            light = int(parts[0])
                            motion = int(parts[1])
                        except ValueError:
                            continue

                        if not (0 <= light <= 1023):
                            continue

                        if motion not in (0, 1):
                            continue

                        timestamp = time.time()

                        message = (
                            f"{timestamp:.6f},"
                            f"{light},{motion}\n"
                        )

                        conn.sendall(message.encode())

                        print(message.strip())

            except (BrokenPipeError, ConnectionResetError):
                print("Client disconnected")

except KeyboardInterrupt:
    print("\nServer stopped")

finally:
    ser.close()
