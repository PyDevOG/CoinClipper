# CoinClipper

**CoinClipper** is a stealth-focused clipboard monitoring tool written in C#. It detects cryptocurrency wallet addresses in clipboard content and replaces them with predefined attacker-controlled addresses. This behavior mimics real-world clipboard hijackers used in active threat campaigns targeting cryptocurrency users.

> ⚠️ **This tool is for educational and authorized penetration testing use only.** Running this code on machines you do not own or do not have explicit permission to test is **illegal and unethical**.

---

## 🚀 Features

- 🔎 **Clipboard Monitoring**: Monitors clipboard content for a variety of cryptocurrency address patterns in real-time.
- 🔁 **Address Replacement**: Automatically swaps detected addresses with attacker-specified ones.
- 👻 **Stealth Techniques**:
  - Hidden windowless operation via background thread.
  - Delayed execution using randomized sleep and CPU-burn routines (`Snoozy` class).
- 🧬 **Persistence Mechanism**:
  - Self-replication into `AppData\Microsoft\SystemData` with randomized executable name.
  - Generates `.ps1` and `.vbs` scripts for execution from the user’s Startup folder.

---

## 🧠 Supported Cryptocurrencies

| Coin       | Regex Pattern |
|------------|---------------|
| **Bitcoin** (BTC)       | `\b(bc1|[13])[a-zA-HJ-NP-Z0-9]{26,45}\b` |
| **Ethereum** (ETH)      | `\b0x[a-fA-F0-9]{40}\b` |
| **TRON** (TRC20)        | `T[A-Za-z1-9]{33}` |
| **Litecoin** (LTC)      | `\b(L|M)[a-km-zA-HJ-NP-Z1-9]{26,33}\b` |
| **Dogecoin** (DOGE)     | `\bD[a-km-zA-HJ-NP-Z1-9]{33}\b` |
| **Monero** (XMR)        | `\b4[0-9AB][1-9A-HJ-NP-Za-km-z]{93}\b` |
| **Ripple** (XRP)        | `\br[1-9A-HJ-NP-Za-km-z]{24,34}\b` |

---

## 🛠 Project Structure

```plaintext
CoinClipper/
├── Program.cs             // Main entry point with randomized sleep and clipboard thread logic
├── Clip.cs                // Core logic for clipboard regex matching and replacement
├── ClipboardFunc.cs       // Safe clipboard read/write utilities
├── ClipboardMonitorForm.cs // Hidden WinForms listener using WM_CLIPBOARDUPDATE
├── Snoozy.cs              // Randomized delays and CPU simulation (anti-analysis)
├── Copy.cs                // Self-copy, persistence, and startup logic


⚠️ Legal Disclaimer
This software is provided for educational purposes only and is intended solely for use in authorized penetration testing environments, such as virtual labs, red team simulations, or malware research.

Do not deploy or distribute this software in production environments or on systems without proper consent. Misuse of this tool may violate computer misuse laws and carry significant legal penalties.

📚 Credits
Regex patterns sourced from research on common cryptocurrency address formats.

Persistence techniques inspired by real-world malware analysis.

Clipboard interception via native Win32 messaging and AddClipboardFormatListener.

🧩 Future Enhancements (Optional)
Add dynamic address fetch from C2 or encrypted config.

Implement address whitelist/blacklist logic.

Obfuscation support or integration with packers like ConfuserEx.




SCAN RESULTS: https://websec.net/scanner/result/525e6e46-a10f-4c62-ab50-0eb1846f2256

