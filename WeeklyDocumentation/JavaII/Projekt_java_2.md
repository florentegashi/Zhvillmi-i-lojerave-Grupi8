# 🎮 Loja The Lost Kingdom

### Përshkrimi i Problemit

Të zhvillohet një lojë aventurë 3D në të cilën lojtari duhet të eksplorojë një mbretëri të harruar, të kapërcejë pengesa dhe të mbledhë **tri kristale magjike** për të rikthyer dritën dhe jetën në mbretëri. Loja kërkon orientim në hapësirë, koordinim të lëvizjeve dhe zgjidhje të sfidave gjatë kalimit nëpër tri nivele.

---

### Lojtari i Synuar

Lojtarë që preferojnë lojëra aventurë, eksplorimi dhe platformer 3D, të cilët pëlqejnë sfidat, zbulimin e ambienteve të reja dhe përfundimin e niveleve.

---

### Qëllimi i Lojtarit

Të eksplorojë tri zona të mbretërisë, të shmangë pengesat dhe të mbledhë **tri kristale magjike**, nga një në secilin nivel, për të çliruar mbretërinë nga mallkimi.

---

### Mekanika Kryesore

Lojtari kontrollon një personazh 3D që mund të **ecë, vrapojë, kërcejë dhe të ndërveprojë me objekte**. Gjatë eksplorimit, ai duhet të kapërcejë pengesa, të gjejë kristalet dhe të arrijë në pikën përfundimtare të secilit nivel.

---

### Rregullat Kryesore

- **Lëvizja:** Lojtari mund të lëvizë në ambientin 3D dhe të kërcejë për të kaluar pengesat.
- **Nivelet:** Loja përmban tri nivele: Mystic Forest, Ancient Cave dhe Lost Castle.
- **Kristalet:** Në secilin nivel gjendet një kristal magjik që duhet të mblidhet.
- **Pengesat:** Lojtari duhet të shmangë pengesat dhe zonat e rrezikshme për të vazhduar lojën.
- **Përparimi:** Kalimi në nivelin tjetër lejohet vetëm pasi të jetë mbledhur kristali dhe të jetë arritur dalja e nivelit.

---

### Komponenti Logjik / Algoritmik

Komponenti kryesor është **menaxhimi i gjendjes së lojës, detektimi i përplasjeve dhe kontrollimi i progresit ndërmjet niveleve**.

Programi duhet të:

1. Përpunojë komandat e lojtarit për lëvizje dhe kërcim.
2. Kontrollojë përplasjet me terrenin, pengesat dhe objektet.
3. Identifikojë dhe regjistrojë mbledhjen e kristaleve.
4. Verifikojë nëse janë plotësuar kushtet për kalimin në nivelin tjetër.
5. Menaxhojë gjendjen e lojës dhe të kontrollojë kushtet e fitores ose humbjes.

---

### Fitorja dhe Humbja

- **Fitore:** Lojtari mbledh të tri kristalet magjike, përfundon nivelin e tretë dhe rikthen dritën në mbretëri.
- **Humbje:** Lojtari bie në një zonë të rrezikshme ose përplaset me pengesa vdekjeprurëse. Në këtë rast, niveli rifillon nga pika fillestare ose nga një checkpoint.

---

## MVP (Minimum Viable Product)

Versioni minimal funksional i lojës do të përmbajë:

- [ ] Ambient 3D me tri nivele bazike.
- [ ] Personazh të kontrollueshëm nga lojtari.
- [ ] Lëvizje dhe kërcim.
- [ ] Detektim të përplasjeve me terrenin dhe pengesat.
- [ ] Mbledhje të kristaleve magjike.
- [ ] Sistem për kalimin ndërmjet niveleve.
- [ ] Ndërfaqe bazike (Menu, Play, Restart).
- [ ] Kushtin e fitores.
- [ ] Kushtin e humbjes dhe rifillimin e nivelit.

---

### Çfarë NUK Përfshihet në MVP

- Multiplayer.
- Sistem online.
- Login dhe regjistrim.
- Sistem kompleks luftimi.
- Inteligjencë artificiale e avancuar për armiqtë.
- Botë e hapur (Open World).
- Nivele shtesë përtej tri niveleve të planifikuara.
- Funksionalitete të avancuara grafike që nuk janë të nevojshme për versionin bazë.

---

### Rreziqet Kryesore

1. **Implementimi i Mekanikave 3D:** Vështirësi në realizimin e lëvizjes së personazhit, kërcimit dhe detektimit korrekt të përplasjeve me objektet dhe pengesat.

2. **Menaxhimi i Niveleve dhe Logjikës:** Sigurimi që kristalet regjistrohen saktë, kalimi ndërmjet niveleve funksionon dhe kushtet e fitores ose humbjes kontrollohen pa gabime.

3. **Menaxhimi i Ekipit dhe Kohës:** Ndarja e detyrave ndërmjet tre anëtarëve, integrimi i pjesëve të zhvilluara në Unity dhe përfundimi i projektit brenda afatit të semestrit.
