Loja 2048
Problemi
Të zhvillohet një lojë puzzle në të cilën lojtari duhet të marrë vendime strategjike për të bashkuar pllakat dhe për të arritur vlerën 2048, duke menaxhuar hapësirën e kufizuar të tabelës.
Lojtari i synuar
Lojtarë që preferojnë lojëra puzzle dhe lojëra që kërkojnë logjikë, planifikim dhe vendimmarrje.
Qëllimi i lojtarit
Të bashkojë pllakat me vlera të njëjta dhe të arrijë pllakën me vlerën 2048.
Mekanika kryesore
Lojtari lëviz të gjitha pllakat në tabelë në një nga katër drejtimet: lart, poshtë, majtas ose djathtas. Pllakat me të njëjtën vlerë bashkohen kur përplasen gjatë një lëvizjeje.
Rregullat kryesore
1.	Loja zhvillohet në një tabelë 4×4.
2.	Pllakat mund të lëvizin në katër drejtime.
3.	Dy pllaka me të njëjtën vlerë mund të bashkohen.
4.	Pas një lëvizjeje të vlefshme krijohet një pllakë e re.
5.	Loja përfundon kur nuk ka më lëvizje të vlefshme.
Komponenti logjik/algoritmik
Komponenti kryesor është algoritmi për përpunimin e lëvizjeve dhe bashkimin e pllakave. Programi duhet të analizojë gjendjen e tabelës, të zhvendosë pllakat, të kryejë bashkimet sipas rregullave dhe të kontrollojë nëse ekziston një lëvizje e vlefshme.
Fitorja dhe humbja
Fitore: lojtari arrin pllakën me vlerën 2048.
Humbje: nuk ekziston më asnjë lëvizje e vlefshme në tabelë.
MVP
Versioni minimal do të përmbajë:
•	tabelën e lojës;
•	lëvizjen e pllakave;
•	bashkimin e pllakave;
•	gjenerimin e pllakave të reja;
•	sistemin e pikëve;
•	kushtin e fitores;
•	kushtin e humbjes;
•	mundësinë për fillimin e një loje të re.
Çfarë nuk përfshihet në MVP
•	multiplayer;
•	sistem online;
•	login dhe regjistrim;
•	ruajtje online e rezultateve;
•	nivele të shumta të lojës;
•	funksionalitete të avancuara që nuk janë të nevojshme për versionin bazë.
Rreziqet kryesore
Rreziku kryesor është implementimi korrekt i logjikës së lëvizjes dhe bashkimit të pllakave, veçanërisht në rastet kur ka disa pllaka të njëjta në të njëjtin drejtim. Një rrezik tjetër është menaxhimi i kohës dhe integrimi i pjesëve të zhvilluara nga anëtarët e ekipit.

