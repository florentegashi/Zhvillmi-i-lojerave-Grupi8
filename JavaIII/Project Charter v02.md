# PROJECT CHARTER v0.2

## Loja 2048

---

## Përshkrimi i projektit

2048 është një lojë puzzle në të cilën lojtari lëviz pllakat në katër drejtime dhe bashkon pllaka me vlerë të njëjtë. Qëllimi kryesor është krijimi i pllakës me vlerën **2048**, duke menaxhuar hapësirën e kufizuar të tabelës dhe duke planifikuar lëvizjet.

---

## Problemi

Projekti synon të krijojë një lojë puzzle të thjeshtë për t'u kuptuar, por që kërkon mendim logjik, planifikim dhe vendimmarrje. Lojtari duhet të zgjedhë lëvizjet në mënyrë strategjike, sepse çdo lëvizje ndryshon gjendjen e tabelës dhe mund të ndikojë në mundësitë e ardhshme.

---

## Lojtari i synuar

Lojtari kryesor është një përdorues që pëlqen lojërat puzzle dhe lojërat që kërkojnë logjikë dhe strategji.

### Profili funksional i lojtarit

* **Niveli i aftësisë:** Fillestar deri në mesatar.
* **Motivimi:** Sfidë, argëtim, zgjidhje problemi dhe përmirësim i rezultatit.
* **Pritshmëritë:** Rregulla të qarta, kontrolle të thjeshta dhe reagim i menjëhershëm nga loja.
* **Platforma:** Kompjuter/Desktop.
* **Konteksti i përdorimit:** Lojë individuale, kryesisht në kohë të lirë dhe në seanca të shkurtra.
* **Nevoja kryesore:** Lojtari duhet ta kuptojë lehtë qëllimin, rregullat dhe rezultatin e veprimeve të tij.

---

## Qëllimi i lojtarit

Qëllimi i lojtarit është të bashkojë pllakat me vlera të njëjta dhe të krijojë pllakën **2048**, duke fituar sa më shumë pikë dhe duke shmangur mbushjen e tabelës.

---

## Mekanika kryesore

Lojtari mund të lëvizë pllakat në katër drejtime:

* Lart
* Poshtë
* Majtas
* Djathtas

Kur dy pllaka me të njëjtën vlerë përplasen gjatë një lëvizjeje, ato bashkohen në një pllakë me vlerë më të madhe.

Pas një lëvizjeje të vlefshme krijohet një pllakë e re në një hapësirë të lirë.

---

## Rregullat kryesore

1. Loja zhvillohet në një tabelë **4×4**.
2. Pllakat mund të lëvizin në katër drejtime.
3. Pllakat me të njëjtën vlerë mund të bashkohen.
4. Pas një lëvizjeje të vlefshme krijohet një pllakë e re.
5. Loja përfundon kur nuk ka më lëvizje të vlefshme.
6. Lojtari fiton kur krijon pllakën me vlerën **2048**.

---

## Komponenti logjik dhe algoritmik

Pjesa kryesore logjike e projektit është përpunimi i lëvizjeve të lojtarit dhe bashkimi i pllakave.

Sistemi duhet të:

* analizojë gjendjen aktuale të tabelës;
* zhvendosë pllakat sipas drejtimit të zgjedhur;
* identifikojë pllakat që mund të bashkohen;
* realizojë bashkimin sipas rregullave;
* llogarisë pikët;
* krijojë pllakën e re pas një lëvizjeje të vlefshme;
* kontrollojë nëse lojtari ka arritur 2048;
* kontrollojë nëse ekziston ende ndonjë lëvizje e vlefshme.

---

## Feedback-u i sistemit

Lojtari duhet të marrë feedback të qartë pas veprimeve kryesore.

| Veprimi              | Feedback-u minimal                       |
| -------------------- | ---------------------------------------- |
| Lëvizje e vlefshme   | Pllakat zhvendosen dhe tabela ndryshon   |
| Bashkim i pllakave   | Pllakat bashkohen dhe vlera e re shfaqet |
| Rritje e pikëve      | Rezultati përditësohet menjëherë         |
| Lëvizje e pavlefshme | Tabela nuk ndryshon                      |
| Arritja e 2048       | Shfaqet njoftim për fitoren              |
| Mungesa e lëvizjeve  | Shfaqet njoftim për përfundimin e lojës  |

---

## Motivimi dhe sjellja e synuar

Loja synon të nxisë:

* mendimin strategjik;
* planifikimin para çdo lëvizjeje;
* zgjidhjen e problemeve;
* vendimmarrjen;
* përmirësimin e rezultatit;
* vazhdimin e lojës për të arritur një rezultat më të lartë.

Motivimi kryesor i lojtarit është sfida për të arritur **2048** dhe për të përmirësuar rezultatin e tij.

---

## Rreziqet e përvojës dhe masat e zvogëlimit

| Rreziku                                            | Masa e zvogëlimit / Testimi                                                                   |
| -------------------------------------------------- | --------------------------------------------------------------------------------------------- |
| Lojtari nuk i kupton rregullat                     | Të ofrohen udhëzime të shkurtra dhe të testohen me lojtarë fillestarë.                        |
| Lojtari frustrohet pas humbjes                     | Të shfaqet qartë përfundimi i lojës dhe të ofrohet mundësia për të filluar një lojë të re.    |
| Lojtari nuk e kupton nëse lëvizja ishte e vlefshme | Të sigurohet feedback i qartë dhe të testohen raste me lëvizje të vlefshme dhe të pavlefshme. |
| Lojtari nuk e kupton progresin                     | Të shfaqet rezultati dhe vlera e pllakave në mënyrë të qartë.                                 |

---

## Çfarë mësuam nga analiza e Minecraft Education

Nga analiza e **Minecraft Education** u mor parimi se loja duhet të ketë **qëllim të qartë, feedback të menjëhershëm dhe progres të dukshëm**.

Ky parim u transferua në projektin 2048 duke përcaktuar që lojtari duhet ta kuptojë menjëherë rezultatin e çdo lëvizjeje, bashkimin e pllakave dhe ndryshimin e pikëve. Gjithashtu, qëllimi për të arritur 2048 duhet të jetë i qartë gjatë gjithë lojës.

---

## MVP – Minimum Viable Product

Versioni minimal i lojës do të përmbajë:

* tabelën 4×4;
* lëvizjen e pllakave;
* bashkimin e pllakave;
* gjenerimin e pllakave të reja;
* sistemin e pikëve;
* kontrollin e fitores;
* kontrollin e humbjes;
* mundësinë për të filluar lojë të re;
* feedback bazë për veprimet e lojtarit.

---

## Çfarë nuk përfshihet në MVP

Në versionin fillestar nuk përfshihen:

* multiplayer;
* sistem online;
* login dhe regjistrim;
* ruajtje online e rezultateve;
* nivele të shumta;
* funksionalitete të tjera që nuk janë të nevojshme për versionin bazë të lojës.

---

## Platforma dhe teknologjia

* **Platforma:** Desktop/PC
* **Engine:** Unity

---

## Ndarja e përgjegjësive

Projekti zhvillohet nga një ekip prej tre anëtarësh:

* **Logic, Algorithm & Software** – zhvillimi dhe testimi i logjikës së lojës.
* **Game Design, Architecture & Interaction** – dizajni, mekanikat dhe ndërveprimi me lojtarin.
* **QA & Project** – testimi, dokumentimi, identifikimi i rreziqeve dhe organizimi i projektit.

---

## Rreziqet e projektit

Rreziku kryesor teknik është implementimi korrekt i logjikës së lëvizjes dhe bashkimit të pllakave, veçanërisht në rastet kur ekzistojnë disa pllaka me vlera të njëjta.

Një tjetër rrezik është menaxhimi i kohës dhe integrimi i pjesëve të zhvilluara nga tre anëtarët e ekipit.

---

## Ndryshimet nga Project Charter v0.1

Në versionin **v0.2** janë shtuar dhe përcaktuar më qartë:

* profili funksional i lojtarit;
* niveli i aftësisë së lojtarit;
* motivimi dhe pritshmëritë e lojtarit;
* platforma dhe konteksti i përdorimit;
* feedback-u minimal i sistemit;
* rreziqet e përvojës dhe mënyrat e testimit;
* parimi i marrë nga analiza e Minecraft Education.

Pjesët kryesore të projektit, mekanika bazë dhe MVP-ja mbeten të njëjta me versionin **v0.1**.

---

## Përfundim

2048 është zgjedhur si një lojë puzzle që kombinon mekanika të thjeshta me kërkesa logjike dhe strategjike. Analiza e lojtarit dhe kontekstit të përdorimit ndihmon në krijimin e një loje të kuptueshme, të parashikueshme dhe me feedback të qartë. Në këtë fazë, fokusi i ekipit është përcaktimi i saktë i përvojës së lojtarit dhe i kërkesave që duhet të plotësojë versioni fillestar i lojës.
