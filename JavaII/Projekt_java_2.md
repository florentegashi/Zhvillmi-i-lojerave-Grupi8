# 🎮 Loja 2048

###  Përshkrimi i Problemit
Të zhvillohet një lojë puzzle në të cilën lojtari duhet të marrë vendime strategjike për të bashkuar pllakat dhe për të arritur vlerën **2048**, duke menaxhuar hapësirën e kufizuar të tabelës.
---
###  Lojtari i Synuar
Lojtarë që preferojnë lojëra puzzle dhe lojëra që kërkojnë logjikë, planifikim dhe vendimmarrje.
---
### Qëllimi i Lojtarit
Të bashkojë pllakat me vlera të njëjta dhe të arrijë pllakën me vlerën **2048**.
---
### Mekanika Kryesore
Lojtari lëviz të gjitha pllakat në tabelë në një nga katër drejtimet: **lart**, **poshtë**, **majtas** ose **djathtas**. Pllakat me të njëjtën vlerë bashkohen kur përplasen gjatë një lëvizjeje.
---
### Rregullat Kryesore
* **Tabela:** Loja zhvillohet në një tabelë `4×4`.
* **Lëvizja:** Pllakat mund të lëvizin në katër drejtime.
* **Bashkimi:** Dy pllaka me të njëjtën vlerë mund të bashkohen.
* **Gjenerimi:** Pas çdo lëvizjeje të vlefshme krijohet një pllakë e re.
* **Përfundimi:** Loja përfundon kur nuk ka më lëvizje të vlefshme.
---
### Komponenti Logjik / Algoritmik
Komponenti kryesor është algoritmi për përpunimin e lëvizjeve dhe bashkimin e pllakave. Programi duhet të:
1. Analizojë gjendjen e tabelës.
2. Zhvendosë pllakat në drejtimin e zgjedhur.
3. Kryejë bashkimet sipas rregullave.
4. Kontrollojë nëse ekziston ndonjë lëvizje e vlefshme.
---
### Fitorja dhe Humbja
* **Fitore:** Lojtari arrin pllakën me vlerën **2048**.
* **Humbje:** Nuk ekziston më asnjë lëvizje e vlefshme në tabelë.
---
## MVP (Minimum Viable Product)
Versioni minimal i lojës do të përmbajë:
- [x] Tabela e lojës (`4×4`)
- [x] Lëvizja e pllakave
- [x] Bashkimi i pllakateve
- [x] Gjenerimi i pllakave të reja
- [x] Sistemi i pikëve
- [x] Kushti i fitores
- [x] Kushti i humbjes
- [x] Mundësia për të filluar një lojë të re (Reset)
---
### Çfarë NUK Përfshihet në MVP
- Multiplayer
- Sistem online
- Login dhe regjistrim
- Ruajtje online e rezultateve
- Nivele të shumta të lojës
- Funksionalitete të tjera të avancuara që nuk janë të nevojshme për versionin bazë
---
### Rreziqet Kryesore
1. **Logjika e Algoritmit:** Implementimi korrekt i logjikës së lëvizjes dhe bashkimit të pllakave, veçanërisht në rastet kur ka disa pllaka me vlerë të njëjtë në të njëjtin rresht apo kollonë.
2. **Menaxhimi i Ekipit:** Menaxhimi i kohës dhe integrimi pa probleme i pjesëve të zhvilluara nga anëtarët e ndryshëm të ekipit.
