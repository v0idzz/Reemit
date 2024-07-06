$OpcodeFile = "Opcodes.txt"
$OpcodeTable = @()
$ExtendedOpcodeTable = @()

Get-Content $OpcodeFile | %{
	$P = $_.Split(' ')

	if ($P.Length -eq 2) {
		$OpcodeTable += @{ Opcode = $P[0]; Mnemonic = $P[1] }
	} else {
		$ExtendedOpcodeTable += @{ Opcode = $P[1]; Mnemonic = $P[2] }
	}
}

function Create-Enum {
	param($EnumName, $OpcodeTable, $IsExtended)
	$CS = ""
	$CS += "public enum " + $EnumName + " : byte`r`n"
	$CS += "{`r`n"

	$OpcodeTable | %{
		$CS += "    [CilMnemonic(`"$($_.Mnemonic)`")]`r`n"
		$CS += "    $($_.Mnemonic.TrimEnd(".").Replace(".", "_")) = $($_.Opcode),`r`n"
	}

	if ($IsExtended -eq $False) {
		$CS += "    Extended = 0xFE,`r`n"
	}

	$CS += "}`r`n"

	return $CS
}

$Enum1 = Create-Enum "Opcode" $OpcodeTable $False
$Enum2 = Create-Enum "ExtendedOpcode" $ExtendedOpcodeTable $True
Write-Host $Enum2

#$OpcodeTable | ConvertTo-Json | Write-Host
#$ExtendedOpcodeTable | ConvertTo-Json | Write-Host
