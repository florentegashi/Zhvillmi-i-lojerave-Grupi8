# GAME DESIGN DOCUMENT (GDD v0.1)

**Lënda:** Zhvillimi i Lojërave – Grupi 8  
**Projekti:** The Lost Kingdom – 3D Adventure  
**Ekipi:** Aldrinë Feka, Florentë Gashi, Rinora Visoka  
**PBT 3:** Java 4 – Dizajnimi i Lojërave  
**Statusi:** Specifikim fillestar; në pritje të implementimit dhe testimit

## 1. Përmbledhja dhe objektivi i sprintit
The Lost Kingdom është aventurë 3D single-player. Një mallkim ka errësuar mbretërinë; lojtari eksploron tri zona dhe mbledh nga një kristal magjik për ta rikthyer dritën. **Sprint Goal:** të formalizohen cikli bazë i lojës, gjendjet, rregullat e testueshme, kushtet e përfundimit dhe rastet e testimit, para implementimit të plotë.

## 2. Core Gameplay Loop
1. **Observe State:** Lojtari sheh terrenin, pengesat, treguesin e kristaleve dhe drejtimin e mundshëm.
2. **Player Decision / Input:** Zgjedh drejtimin e lëvizjes, vrapimin ose kërcimin.
3. **Rule Check:** Sistemi kontrollon tokën, përplasjet, zonat e rrezikshme dhe kontaktin me kristalin/daljen.
4. **State Change:** Pozicioni ndryshon; kristali shënohet si i mbledhur ose lojtari rikthehet në checkpoint.
5. **Feedback:** Animacion, tingull/efekt i kristalit, tregues progresi dhe mesazh për dalje të bllokuar.
6. **Next Decision:** Lojtari vazhdon eksplorimin ose kalon në nivelin tjetër.

## 3. Modeli i gjendjeve (Game State Model)
| Variabla | Vlera fillestare | Kufizimi / kuptimi |
|---|---|---|
| `gamePhase` | `MainMenu` | MainMenu, Playing, Paused, LevelComplete, GameOver, Victory |
| `currentLevel` | 1 | Numër i plotë 1–3 |
| `playerPosition` | Spawn i nivelit | Pozicion 3D i vlefshëm |
| `isGrounded` | `true` | Boolean; lejon kërcim vetëm në tokë |
| `collectedCrystals` | 0 | Numër i plotë 0–3 |
| `crystalCollectedInLevel` | `false` | Boolean; vetëm një kristal për nivel |
| `exitUnlocked` | `false` | Aktivizohet pas mbledhjes së kristalit |
| `checkpointPosition` | Spawn i nivelit | Pozicion rikthimi |
| `isPlayerAlive` | `true` | Boolean; ndryshon në zonë vdekjeprurëse |

### 3.1 Tranzicionet e gjendjeve
| Nga | Ngjarja | Kushti | Në |
|---|---|---|---|
| MainMenu | Play | Gjithmonë | Playing (niveli 1) |
| Playing | Pause | Loja aktive | Paused |
| Paused | Resume | Loja e pauzuar | Playing |
| Playing | Prek kristalin | Ende i pambledhur | Playing; progres +1 |
| Playing | Prek daljen | Kristali i nivelit i mbledhur; niveli < 3 | LevelComplete → Playing (niveli pasues) |
| Playing | Prek daljen finale | Kristali i nivelit 3 i mbledhur; gjithsej 3 | Victory |
| Playing | Bie në rrezik | Zonë vdekjeprurëse | GameOver / Restart checkpoint |

## 4. Rregulla të testueshme IF/WHEN → THEN
| ID | WHEN / IF | THEN |
|---|---|---|
| R-01 Movement | WHEN lojtari jep input lëvizjeje gjatë Playing | Personazhi lëviz në drejtimin e kërkuar, i kufizuar nga përplasjet. |
| R-02 Jump | IF shtypet Jump AND `isGrounded=true` | Kryhet një kërcim; kërcimi i dyfishtë në ajër nuk lejohet në MVP. |
| R-03 Crystal | IF lojtari prek kristalin AND ai nuk është mbledhur | Kristali çaktivizohet, progresi rritet vetëm një herë, shfaqet feedback. |
| R-04 Exit | IF lojtari prek daljen AND kristali i nivelit është mbledhur | Ngarkohet niveli pasues, ose Victory në nivelin 3. |
| R-05 Locked Exit | IF lojtari prek daljen pa kristal | Dalja mbetet e bllokuar dhe shfaqet udhëzim. |
| R-06 Hazard | IF lojtari hyn në zonë vdekjeprurëse | Loja shfaq dështimin dhe mundëson rifillim nga checkpoint/spawn. |

## 5. Kushtet e fitores, humbjes dhe rastet kufitare
**Fitore:** janë mbledhur të tri kristalet dhe është arritur dalja e nivelit të tretë.  
**Humbje lokale:** lojtari bie në humnerë ose prek rrezik vdekjeprurës; ofrohet Restart.  
**Përfundim manual:** lojtari kthehet në menu ose mbyll lojën.

**Edge cases:** (a) prekja e kristalit disa herë nuk e dyfishon numërimin; (b) dalja nuk aktivizohet pa kristal; (c) kërcimi nuk përsëritet në ajër; (d) përplasja nuk lejon kalimin përmes murit; (e) niveli 3 nuk provon të ngarkojë nivel 4.

## 6. Paper Prototype / Simulimi i mekanikës
**Prototip i planifikuar:** skicë e tri skenave me Spawn → Pengesa → Kristal → Dalje. Testohet rrjedha me kartela: lojtari zgjedh veprimin, ekipi zbaton rregullat dhe shënon gjendjen e re. Prototipi fizik/Unity dhe rezultatet e tij do të dokumentohen pasi të realizohen.

## 7. Raste testimi të planifikuara
| ID | Skenari | Input | Rezultati i pritshëm | Statusi |
|---|---|---|---|---|
| TC-01 | Lëvizja | WASD në terren të lirë | Personazhi lëviz në drejtimin përkatës | Not Run |
| TC-02 | Kërcim në ajër | Jump dy herë para uljes | Nuk kryhet kërcim i dytë | Not Run |
| TC-03 | Mbledhje e përsëritur | Prek të njëjtin kristal disa herë | Numërimi rritet vetëm një herë | Not Run |
| TC-04 | Dalje e bllokuar | Prek daljen pa kristal | Nuk ndryshon niveli; jepet feedback | Not Run |
| TC-05 | Kalim niveli | Mblidh kristalin, prek daljen | Ngarkohet niveli pasues | Not Run |
| TC-06 | Fitore finale | Mblidh të tre kristalet, prek daljen finale | Shfaqet Victory | Not Run |
| TC-07 | Rrezik | Bie në humnerë | Shfaqet dështimi / rifillimi | Not Run |

## 8. Playtesting me ekip tjetër
**Status:** ende i parealizuar. Gjatë testimit do të vlerësohen kuptueshmëria e kontrolleve, orientimi, dukshmëria e kristalit, vështirësia dhe feedback-u. Për secilin problem do të regjistrohen hapat, rezultati i pritur, rezultati real, propozimi i ndryshimit dhe retesti. Nuk raportohen rezultate të supozuara.

## 9. Raporti 3-minutësh i javës (draft)
- **Sprint Goal:** Formalizimi i core loop, gjendjeve dhe rregullave të testueshme.
- **Artefakt i përgatitur:** GDD v0.1 me tabela të gjendjeve, rregullave dhe test cases.
- **Evidenca për t'u shtuar:** linku i commit-it, screenshot i prototipit dhe regjistri i testeve.
- **Rreziku kryesor:** përplasjet 3D dhe përmasat e tri niveleve brenda semestrit.
- **Objektivi vijues:** prototip i luajtshëm i Mystic Forest me lëvizje, kristal dhe dalje.

## 10. Shënime për MVP
Platforma: Windows desktop; Unity + C#. Nivelet: Mystic Forest, Ancient Cave, Lost Castle. Jashtë MVP: multiplayer, open world, armiq me AI të avancuar dhe sistem kompleks luftimi. Rolet e ekipit caktohen sipas rotacionit javor. Burimet e jashtme dhe mjetet AI të përdorura dokumentohen në repository.
