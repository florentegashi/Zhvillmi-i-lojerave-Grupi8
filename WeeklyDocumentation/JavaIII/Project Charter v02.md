# PROJECT CHARTER v0.2

## 🎮 Loja The Lost Kingdom

---

## Përshkrimi i projektit

**The Lost Kingdom** është një lojë aventurë 3D në të cilën lojtari kontrollon një aventurier që gjendet në një mbretëri të harruar, ku një mallkim misterioz ka zhdukur dritën dhe jetën. Për të shpëtuar mbretërinë, lojtari duhet të eksplorojë tri nivele, të kapërcejë pengesa dhe të mbledhë **tri kristale magjike**.

Çdo kristal i mbledhur e afron lojtarin drejt rikthimit të mbretërisë.

---

## Problemi

Projekti synon të krijojë një lojë aventurë 3D që kombinon eksplorimin, orientimin hapësinor dhe kapërcimin e pengesave. Lojtari duhet të kontrollojë lëvizjet e personazhit, të identifikojë rrugët e kalimit dhe të mbledhë kristalet magjike për të përparuar.

Çdo nivel paraqet sfida të ndryshme, ndërsa veprimet e lojtarit ndikojnë në progresin dhe përfundimin e lojës.

---

## Lojtari i synuar

Lojtari kryesor është një përdorues që preferon lojëra aventurë, eksplorimi dhe platformer 3D, me sfida që kërkojnë koordinim dhe orientim në ambient.

### Profili funksional i lojtarit

- **Niveli i aftësisë:** Fillestar deri në mesatar.
- **Motivimi:** Eksplorim, argëtim, kapërcim i pengesave, mbledhje e kristaleve dhe përfundim i aventurës.
- **Pritshmëritë:** Kontrolle të thjeshta, lëvizje të qëndrueshme, ambiente tërheqëse dhe feedback i menjëhershëm.
- **Platforma:** Kompjuter/Desktop.
- **Konteksti i përdorimit:** Lojë individuale (Single-player), kryesisht në kohë të lirë.
- **Nevoja kryesore:** Lojtari duhet t'i kuptojë lehtë kontrollet, objektivat e niveleve dhe rezultatet e veprimeve të tij.

---

## Qëllimi i lojtarit

Qëllimi i lojtarit është të eksplorojë tri zona të mbretërisë, të shmangë pengesat dhe të mbledhë **tri kristale magjike**, nga një në secilin nivel.

Pas mbledhjes së kristalit të tretë dhe përfundimit të nivelit final, mallkimi largohet dhe mbretëria rikthehet në gjendjen e saj të mëparshme.

---

## Mekanika kryesore

Lojtari kontrollon një personazh 3D që mund të:

- Ecë dhe të vrapojë në ambient.
- Kërcejë për të kapërcyer pengesat.
- Eksplorojë zona të ndryshme.
- Mbledhë kristale magjike.
- Arrijë në pikën përfundimtare të nivelit.

Loja përmban tri nivele:

1. **Mystic Forest** – Pyll magjik ku lojtari mëson lëvizjen, kërcimin dhe mbledh kristalin e parë.
2. **Ancient Cave** – Shpellë e lashtë me pengesa dhe sfida më të vështira, ku gjendet kristali i dytë.
3. **Lost Castle** – Kështjella e harruar ku lojtari duhet të mbledhë kristalin e fundit dhe të shpëtojë mbretërinë.

---

## Rregullat kryesore

1. Lojtari mund të lëvizë dhe të kërcejë brenda ambientit 3D.
2. Loja përmban **tri nivele**, të cilat përfundohen sipas rendit.
3. Në secilin nivel gjendet një kristal magjik që duhet të mblidhet.
4. Lojtari duhet të shmangë pengesat dhe zonat e rrezikshme.
5. Kalimi në nivelin tjetër lejohet vetëm pas mbledhjes së kristalit dhe arritjes së daljes.
6. Loja fitohet kur mblidhen të tri kristalet dhe përfundohet niveli i tretë.

---

## Komponenti logjik dhe algoritmik

Pjesa kryesore logjike e projektit është **menaxhimi i gjendjes së lojës, detektimi i përplasjeve dhe kontrollimi i progresit ndërmjet niveleve**.

Sistemi duhet të:

- Përpunojë komandat e lojtarit për lëvizje dhe kërcim.
- Kontrollojë përplasjet me terrenin, pengesat dhe objektet.
- Identifikojë kur lojtari prek një kristal magjik.
- Regjistrojë kristalet e mbledhura dhe të parandalojë mbledhjen e përsëritur.
- Kontrollojë nëse janë plotësuar kushtet për kalimin në nivelin tjetër.
- Menaxhojë rifillimin e nivelit pas dështimit.
- Kontrollojë nëse janë mbledhur të tri kristalet.
- Aktivizojë përfundimin e lojës kur plotësohen kushtet e fitores.

---

## Feedback-u i sistemit

Lojtari duhet të marrë feedback të qartë pas veprimeve kryesore.

| Veprimi | Feedback-u minimal |
|---|---|
| Lëvizje dhe kërcim | Personazhi reagon menjëherë ndaj komandave. |
| Mbledhja e kristalit | Kristali zhduket dhe shfaqet njoftim ose efekt vizual. |
| Përplasje me pengesë | Personazhi ndalet ose pengohet të kalojë. |
| Hyrje në zonë të rrezikshme | Lojtari rikthehet në pikën fillestare ose checkpoint. |
| Dalje pa mbledhur kristalin | Shfaqet njoftim se kristali duhet të mblidhet. |
| Përfundimi i nivelit | Shfaqet njoftim dhe ngarkohet niveli tjetër. |
| Mbledhja e tri kristaleve | Shfaqet mesazhi i fitores dhe rikthimi i mbretërisë. |

---

## Motivimi dhe sjellja e synuar

Loja synon të nxisë:

- Eksplorimin e ambienteve të ndryshme.
- Orientimin dhe koordinimin në hapësirë 3D.
- Planifikimin e lëvizjeve për të shmangur pengesat.
- Zgjidhjen e sfidave gjatë niveleve.
- Përmirësimin e aftësive përmes përsëritjes.
- Vazhdimin e aventurës për të zbuluar nivelin tjetër.

Motivimi kryesor është **mbledhja e tri kristaleve magjike dhe shpëtimi i mbretërisë**, duke përjetuar sfida gradualisht më të vështira.

---

## Rreziqet e përvojës dhe masat e zvogëlimit

| Rreziku | Masa e zvogëlimit / Testimi |
|---|---|
| Lojtari nuk i kupton kontrollet. | Të shfaqen udhëzime të shkurtra në fillim dhe të testohen me lojtarë fillestarë. |
| Lojtari nuk e di ku duhet të shkojë. | Të përdoren shenja orientuese dhe elemente vizuale që tregojnë drejtimin. |
| Lojtari frustrohet nga pengesat. | Vështirësia të rritet gradualisht dhe të ofrohet mundësia e rifillimit. |
| Lojtari nuk e kupton nëse ka mbledhur kristalin. | Të shfaqet efekt vizual, tingull ose njoftim pas mbledhjes. |
| Lojtari nuk e kupton progresin. | Të shfaqet niveli aktual dhe numri i kristaleve të mbledhura. |
| Lëvizja ose kamera e vështirëson orientimin. | Të testohen kontrollet dhe pozicionimi i kamerës për të siguruar dukshmëri të mirë. |

---

## Çfarë mësuam nga analiza e Minecraft Education

Nga analiza e **Minecraft Education** u mor parimi se një lojë duhet të ketë **qëllime të qarta, eksplorim të kuptueshëm, feedback të menjëhershëm dhe progres të dukshëm**.

Ky parim u transferua në The Lost Kingdom duke përcaktuar që lojtari duhet ta kuptojë qëllimin e secilit nivel, të marrë feedback gjatë mbledhjes së kristaleve dhe të shohë progresin e tij gjatë aventurës.

Gjithashtu, niveli i parë do të shërbejë si një hyrje praktike për mësimin e kontrolleve dhe mekanikave bazë të lojës.

---

## MVP – Minimum Viable Product

Versioni minimal i lojës do të përmbajë:

- Ambient 3D me tri nivele bazike.
- Personazh të kontrollueshëm nga lojtari.
- Lëvizje, vrapim dhe kërcim.
- Kamerë që ndjek personazhin.
- Detektim të përplasjeve me terrenin dhe pengesat.
- Mbledhje të tri kristaleve magjike.
- Kontroll të progresit dhe kalim ndërmjet niveleve.
- Ndërfaqe bazike (Menu, Play, Restart).
- Shfaqje të nivelit dhe kristaleve të mbledhura.
- Kushtin e fitores dhe humbjes.
- Feedback bazë vizual për veprimet e lojtarit.

---

## Çfarë nuk përfshihet në MVP

Në versionin fillestar nuk përfshihen:

- Multiplayer.
- Sistem online.
- Login dhe regjistrim.
- Sistem kompleks luftimi.
- Inteligjencë artificiale e avancuar për armiqtë.
- Botë e hapur (Open World).
- Nivele shtesë përtej tri niveleve të planifikuara.
- Personalizim i avancuar i personazhit.
- Funksionalitete grafike të avancuara që nuk janë të nevojshme për versionin bazë.

---

## Platforma dhe teknologjia

- **Platforma:** Desktop/PC (Windows).
- **Engine:** Unity.
- **Gjuha programuese:** C#.
- **Mjedisi i zhvillimit:** Visual Studio.
- **Menaxhimi i versioneve:** Git dhe GitHub.

---

## Ndarja e përgjegjësive

Projekti zhvillohet nga një ekip prej tre anëtarësh:

- **Aldrinë Feka**
- **Florentë Gashi**
- **Rinora Visoka**

Përgjegjësitë organizohen sipas roleve të projektit:

- **Logic, Algorithm & Software** – zhvillimi i logjikës së lëvizjes, përplasjeve, mbledhjes së kristaleve dhe menaxhimit të niveleve.
- **Game Design, Architecture & Interaction** – dizajni i niveleve, mekanikat, ndërfaqja dhe ndërveprimi i lojtarit me ambientin.
- **QA & Project** – testimi, dokumentimi, identifikimi i gabimeve, menaxhimi i rreziqeve dhe organizimi i projektit.

Rolet mund të ndryshojnë sipas rotacionit javor të ekipit.

---

## Rreziqet e projektit

Rreziku kryesor teknik është implementimi korrekt i **lëvizjes së personazhit, kamerës dhe detektimit të përplasjeve** në ambientin 3D.

Një tjetër rrezik është menaxhimi i progresit, në mënyrë që kristalet të regjistrohen saktë dhe kalimi ndërmjet niveleve të funksionojë vetëm kur plotësohen kushtet.

Gjithashtu, krijimi i tri ambienteve 3D mund të kërkojë kohë të konsiderueshme. Për këtë arsye, fillimisht do të zhvillohen nivele të thjeshta funksionale, ndërsa përmirësimet grafike do të realizohen gradualisht.

Rrezik tjetër është menaxhimi i kohës dhe integrimi i pjesëve të zhvilluara nga tre anëtarët e ekipit.

---

## Ndryshimet nga Project Charter v0.1

Në versionin **v0.2** janë shtuar dhe përcaktuar më qartë:

- Profili funksional i lojtarit.
- Niveli i aftësisë së lojtarit.
- Motivimi dhe pritshmëritë e lojtarit.
- Platforma dhe konteksti i përdorimit.
- Feedback-u minimal i sistemit.
- Rreziqet e përvojës dhe mënyrat e testimit.
- Parimi i marrë nga analiza e Minecraft Education.
- Orientimi i lojtarit dhe dukshmëria e progresit.

Pjesët kryesore të projektit, historia, tri nivelet, mekanikat bazë dhe MVP-ja mbeten të njëjta me versionin **v0.1**.

---

## Përfundim

**The Lost Kingdom** është zgjedhur si një lojë aventurë 3D që kombinon eksplorimin, kapërcimin e pengesave dhe menaxhimin e progresit të lojtarit. Analiza e lojtarit dhe kontekstit të përdorimit ndihmon në krijimin e një përvoje të kuptueshme, motivuese dhe me feedback të qartë.

Në këtë fazë, fokusi i ekipit është përcaktimi i saktë i përvojës së lojtarit, mekanikave kryesore dhe kërkesave që duhet të plotësojë versioni fillestar i lojës, përpara zhvillimit dhe integrimit të funksionaliteteve në Unity.
