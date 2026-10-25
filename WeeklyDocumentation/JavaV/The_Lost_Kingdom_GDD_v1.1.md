# THE LOST KINGDOM — GAME DESIGN DOCUMENT (GDD v1.1)

**Lënda:** Zhvillimi i Lojërave · **Grupi:** 8  
**Ekipi:** Aldrinë Feka, Florentë Gashi, Rinora Visoka  
**PBT 4:** Java 5 — Shkrimi i lojërave · Faza II: Product Design  
**Platforma:** Desktop/Windows · **Engine:** Unity · **Gjuha:** C#  
**Statusi:** Specifikim dizajni; implementimi dhe playtest-i kërkojnë verifikim.

## 1. Sprint Goal

Të projektohet një progresion i kuptueshëm në tri faza, me informacion të shpërndarë në kohën e duhur, tri sfida të realizueshme dhe feedback të qartë për suksesin, refuzimin, dështimin dhe rikuperimin, pa ndryshuar mekanikën e GDD v0.1.

## 2. Premisa dhe mekanika e pandryshuar

Një aventurier kërkon tri kristale magjike për të rikthyer dritën në mbretërinë e mallkuar. Lojtari ecën/vrapon, kërcen, shmang pengesat, mbledh **një kristal për nivel** dhe arrin daljen. Dalja aktivizohet vetëm kur kristali i nivelit është mbledhur. Rreziku vdekjeprurës rikthen lojtarin në fillim të nivelit ose te checkpoint-i i aktivizuar. Pas kristalit të tretë dhe daljes së fundit, loja fitohet.

**Core loop:** Vëzhgo ambientin → vendos rrugën → lëviz/kërce → sistemi kontrollon përplasjet dhe mbledhjen → merr feedback → arrij kristalin dhe daljen → vazhdo në nivelin tjetër.

## 3. Progression Map

| Faza | Niveli | Aftësia | Kushti i kalimit | Humbja / Rikuperimi |
|---|---|---|---|---|
| Onboarding | 1 — Mystic Forest | Lëvizje, kërcim, njohja e kristalit dhe daljes | Kristali 1 + arritja te dalja | Rënia në zonë të rrezikshme → rifillim niveli/checkpoint |
| Core challenge | 2 — Ancient Cave | Kombinim kërcimesh dhe zgjedhje e rrugës | Kristali 2 + arritja te dalja | Rënia → rifillim niveli/checkpoint |
| Advanced challenge | 3 — Lost Castle | Zbatim i pavarur i të gjitha aftësive në sekuencë më komplekse | Kristali 3 + arritja te dalja → fitore | Rënia → rifillim niveli/checkpoint |

**Rrjedha:** Main Menu → Mystic Forest → Ancient Cave → Lost Castle → Victory. Në çdo nivel: `Playing → (Hazard → Respawn → Playing)` ose `Playing → (Crystal Collected → Exit Unlocked → Level Complete)`. `Pause → Resume` kthen në lojë pa ndryshuar progresin.

**Rritja e vështirësisë:** Niveli 1 ka një kërcim të izoluar dhe rrugë të dukshme; niveli 2 kërkon dy kërcime të njëpasnjëshme dhe orientim në një kthesë; niveli 3 kombinon disa segmente, kthesa dhe platforma me distanca të ndryshme. Nuk ndryshojnë komandat, kushti i kristalit apo rregullat e humbjes.

## 4. Shpërndarja e informacionit (Information Design)

| Momenti | Informacioni i dhënë | Informacioni që zbulohet nga lojtari |
|---|---|---|
| Para veprimit të parë | Objektivi: “Gjej kristalin dhe arrij daljen”; WASD/lëvizja, Space/kërcimi; numëruesi 0/3 | Rruga konkrete përmes pyllit |
| Pas lëvizjes/kërcimit | Reagim i menjëhershëm i personazhit dhe kamerës | Distanca e sigurt e kërcimit |
| Kur preket kristali | Efekt drite, tingull dhe “Kristali u mblodh (1/3)” | Si të arrijë te dalja |
| Kur provohet dalja pa kristal | “Mblidh kristalin e këtij niveli për të vazhduar” | Vendndodhja e kristalit, duke eksploruar |
| Pas dështimit | “Rashë në zonë të rrezikshme. Provo përsëri.” | Rruga alternative ose koha më e mirë e kërcimit |
| Pas nivelit | “Niveli u përfundua”; progresi X/3; “Next Level” | Sfida e nivelit pasues |

Udhëzimet e kontrollit shfaqen në fillim dhe mbeten të qasshme në Pause/Help. Nuk zbulohet paraprakisht rruga e plotë e zgjidhjes.

## 5. Tri Level Cards

### LC-01 — Mystic Forest (Onboarding)

- **Objektivi:** Mblidh kristalin e pyllit dhe arrij daljen.
- **Aftësia e synuar:** Ecje, orientim, kërcim i vetëm dhe njohja e objektivit.
- **Konfigurimi i propozuar:** Pikënisje S → shteg i drejtë → hendek i ngushtë me platformë të sigurt përtej → kristali C → porta E. Shenja vizuale te hendeku dhe kristali i dukshëm nga platforma.
- **Kufizimet:** Lojtari nuk mund ta kalojë hendekun duke ecur; dalja nuk aktivizohet pa kristalin.
- **Zgjidhje e vlefshme / provë realizueshmërie:** S → ecje deri te buza → kërcim mbi hendek → ulje në platformë → prek C → ecje te E. **Kusht verifikimi në Unity:** gjerësia e hendekut të jetë më e vogël se distanca reale e kërcimit të personazhit, me hapësirë të mjaftueshme për ulje.
- **Fitore / humbje:** C + E = përfundim niveli; rënia = respawn.
- **Feedback:** Tutorial kontrollesh, shkëlqim kristali, mesazh për kristalin dhe daljen.
- **Kushti i kalimit:** `levelCrystalCollected == true && playerAtExit == true`.

### LC-02 — Ancient Cave (Core Challenge)

- **Objektivi:** Gjej kristalin në shpellë dhe dil.
- **Aftësia e synuar:** Dy kërcime të njëpasnjëshme dhe orientim në kthesë.
- **Konfigurimi i propozuar:** S → korridor me kthesë → platforma P1 → P2 mbi dy hendeqe → C në platformën e fundit → E në korridorin pasues.
- **Kufizimet:** Çdo hendek duhet kaluar me kërcim; dalja kërkon kristalin; rënia kthen te checkpoint-i i fundit, nëse është aktivizuar.
- **Zgjidhje e vlefshme / provë realizueshmërie:** S → kthesë → kërcim te P1 → ndalje/orientim → kërcim te P2 → C → E. **Kusht verifikimi:** secila distancë P0–P1 dhe P1–P2 të jetë brenda kapacitetit të kërcimit, me platforma mjaftueshëm të gjera për ulje.
- **Fitore / humbje:** C + E = përfundim; rënia = respawn.
- **Feedback:** Shenja në kthesë, efekt kristali, njoftim “2/3 kristale”.
- **Kushti i kalimit:** `levelCrystalCollected == true && playerAtExit == true`.

### LC-03 — Lost Castle (Advanced Challenge)

- **Objektivi:** Mblidh kristalin e fundit dhe çliro mbretërinë.
- **Aftësia e synuar:** Kombinim i lëvizjes, orientimit dhe kërcimeve në disa segmente.
- **Konfigurimi i propozuar:** S → korridor me kthesë → platformë e gjerë P1 → platformë më e ngushtë P2 → kthesë → P3 → C → porta finale E.
- **Kufizimet:** Rrugë e vazhdueshme vetëm përmes platformave; dalja kërkon kristalin 3; rënia = respawn.
- **Zgjidhje e vlefshme / provë realizueshmërie:** S → P1 → P2 → kthesë → P3 → C → E. **Kusht verifikimi:** çdo hendek veçmas është i kalueshëm; ulja në P2 është e mundshme me kontrollet standarde. Kjo nuk është garanci pa matje në prototip.
- **Fitore / humbje:** C + E = fitore finale; rënia = respawn.
- **Feedback:** Kristali i tretë ndriçon, “3/3 kristale”, animacion/mesazh “Mbretëria u shpëtua!”.
- **Kushti i kalimit:** `totalCrystals == 3 && playerAtFinalExit == true`.

**Shënim për verifikimin:** Këto janë zgjidhje të projektimit, jo rezultate playtest-i. Për secilin nivel duhet matur distanca e kërcimit dhe provuar fizikisht se rruga mund të përfundohet pa mjete debug.

## 6. Feedback Matrix

| Veprimi / ngjarja | Reagimi i sistemit | Feedback-u i dukshëm / audio |
|---|---|---|
| Lëvizje e vlefshme | Përditëson pozicionin, respekton collider-at | Animacion ecjeje/vrapimi dhe lëvizje kamere |
| Kërcim i vlefshëm kur është në tokë | Jep impuls vertikal | Animacion kërcimi / tingull opsional |
| Kërcim i refuzuar në ajër | Nuk ekzekuton kërcim të dytë | Nuk ka impuls shtesë; tutorial “Kërce kur je në tokë” vetëm kur duhet |
| Prekje e kristalit të pambledhur | Regjistron kristalin një herë | Efekt drite, tingull, numërues X/3 |
| Prekje e kristalit të mbledhur | Nuk rrit numëruesin | Nuk shfaqet shpërblim i dytë |
| Hyrje te dalja pa kristal | Refuzon kalimin | “Mblidh kristalin e këtij niveli” |
| Hyrje te dalja me kristal | Përfundon nivelin | “Level Complete”, butoni Next Level |
| Rënie në zonë të rrezikshme | Aktivizon respawn | “Rashë në zonë të rrezikshme. Provo përsëri.” |
| Dalja finale me 3 kristale | Kalon në Victory | “Mbretëria u shpëtua!” dhe efekt drite |

## 7. Dështime, gabime dhe rikuperimi

| Lloji | Shkaku | Rezultati i sistemit | Mesazhi | Rikuperimi |
|---|---|---|---|---|
| Humbje e lojtarit | Bie në hendek / zonë të rrezikshme | Aktivizohet respawn, pa kaluar nivelin | “Rashë në zonë të rrezikshme. Provo përsëri.” | Rikthim te checkpoint-i ose fillimi; provo përsëri |
| Veprim i refuzuar, jo humbje | Arrin te porta pa kristalin | Porta mbetet e mbyllur | “Mblidh kristalin e këtij niveli” | Kthehu, gjej kristalin dhe provo portën sërish |
| Defekt konfigurimi (bug), jo humbje | Hendek më i gjatë se kapaciteti i kërcimit ose kristal jashtë zonës së arritshme | Niveli nuk mund të përfundohet | Për testuesin: “Konfigurim i pavlefshëm i nivelit” | Raporto issue; korrigjo pozicionet/distancat; ritesto nivelin |

**Politikë checkpoint:** Në MVP lejohet rifillimi nga fillimi i nivelit. Checkpoint-et janë opsionale; nëse implementohen, duhet dokumentuar ruajtja e kristalit gjatë respawn-it dhe testuar që kristali nuk numërohet dy herë.

## 8. Playtest Note — formular për t'u plotësuar pas testit real

- **Statusi:** PENDING — nuk është raportuar ende një test me ekip tjetër.
- **Data / ekipi testues:** [plotëso]
- **Mjeti:** [paper prototype / Unity build + versioni]
- **Detyra:** Pa ndihmë verbale, gjej kristalin dhe daljen në nivelin 1.
- **Vëzhgimi konkret (çfarë bëri lojtari):** [plotëso vetëm pas vëzhgimit]
- **Problemi / citimi i testuesit:** [plotëso]
- **Vendimi / ndryshimi i propozuar:** [plotëso]
- **Ritestimi dhe rezultati:** [plotëso]
- **Evidenca:** [link video, screenshot, issue ose shënim i datuar]

## 9. Statusi i prototipit dhe backlog-u

Nuk është dhënë evidencë e verifikuar për gjendjen aktuale të kodit; prandaj asnjë funksionalitet nuk deklarohet si i implementuar ose i testuar.

| Task | Owner (rol) | Evidenca e nevojshme | Statusi |
|---|---|---|---|
| Tutoriali WASD/Space në Mystic Forest | Game Design + Logic | Screenshot/commit dhe provë me lojtar | To Do — për t'u konfirmuar |
| Mesazhi kur porta refuzon hyrjen | Logic | Video/test pa kristal | To Do — për t'u konfirmuar |
| Numëruesi i kristaleve X/3 | Logic | Test që numëron vetëm një herë | To Do — për t'u konfirmuar |
| Verifikimi i tri rrugëve të niveleve | QA + Game Design | Video të përfundimit dhe matje të hendekëve | To Do |
| Playtest me ekip tjetër | QA & Project | Playtest Note me vëzhgim real | To Do |
| Përditësimi i README-së | QA & Project | Commit/link repository | To Do |

**Varësi teknike:** Për tutorialin dhe feedback-un në Unity nevojiten një scene funksionale, PlayerController, trigger-at e kristalit dhe daljes, si dhe një Canvas UI. Përgjegjësit caktohen me emër nga ekipi sipas rotacionit të javës.

## 10. Acceptance Criteria / Definition of Done

- [x] Dokumentohet progresioni në tri faza me të njëjtën mekanikë bazë.
- [x] Dokumentohen tri karta me rrugë të propozuara dhe kushte të kontrollueshme realizueshmërie.
- [x] Dokumentohet Feedback Matrix me veprim të vlefshëm, të refuzuar, sukses dhe dështim.
- [x] Dallohet humbja nga defekti i konfigurimit dhe përcaktohet rikuperimi.
- [ ] Rrugët e tri niveleve verifikohen me prototip ose simulim të matur.
- [ ] Kryhet playtest real dhe regjistrohet vëzhgimi + vendimi.
- [ ] Shtohet të paktën një feedback/udhëzim në prototip, nëse mjedisi është funksional.
- [ ] Përditësohet README me evidencë konkrete dhe lidhje commit-esh.

## 11. Version History / Change Log

| Versioni | Faza | Ndryshimet |
|---|---|---|
| v0.1 | Java 4 / PBT 3 | Mekanika, rregullat, gjendja e lojës, fitore/humbje dhe MVP bazë |
| v1.1 | Java 5 / PBT 4 | Progression Map, tri Level Cards, shpërndarja e informacionit, Feedback Matrix, dështimi/rikuperimi, plan playtest-i dhe backlog |

**Shënim:** Emërtimi v1.1 përdoret sipas kërkesës së PBT 4; është përditësim i draftit të Javës 4 (v0.1).

## 12. Raporti 3-minutësh — strukturë e gatshme

1. **Sprint Goal:** Projektimi i progresionit, informacionit dhe feedback-ut në tri nivele.
2. **Rezultatet:** Progression Map, tri karta niveli, Feedback Matrix dhe GDD v1.1 të dokumentuara; implementimi nuk është verifikuar.
3. **Evidenca:** Ky dokument; shtohen linket e commit-eve, prototipit dhe playtest-it kur të ekzistojnë.
4. **Problem real:** [plotëso pas testimit; mos e paraqit si fakt pa evidencë].
5. **Çfarë mësuam:** Vështirësia mund të rritet me kombinimin e kërcimeve dhe orientimit, pa shtuar komanda të reja.
6. **Blocker/Risk:** Distancat e platformave mund ta bëjnë nivelin të pamundur; duhen matur në Unity.
7. **Objektivi tjetër:** Verifikimi i rrugëve dhe implementimi i feedback-ut të parë, me playtest të dokumentuar.

## 13. Referenca të kursit

- Leslie Stirling, *Game Writing and Interactive Storytelling*, kap. 2.2, f. 139–163 (sipas sprint brief-it).
- Isaac Barry, kap. 2.1, f. 122–126 (sipas sprint brief-it).
- GDD v0.1 dhe Project Charter v0.2 — *The Lost Kingdom*, Grupi 8.
