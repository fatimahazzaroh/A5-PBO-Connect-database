-- Internasional
INSERT INTO AcuanPoin (id_tingkat, id_posisi, poin)
VALUES
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Internasional'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Juara 1/2/3'), 500),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Internasional'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Finalis'), 200),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Internasional'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Peserta'), 50);

-- Nasional DIKTI
INSERT INTO AcuanPoin (id_tingkat, id_posisi, poin)
VALUES
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Nasional DIKTI'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Juara 1/2/3'), 400),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Nasional DIKTI'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Terpilih / Didanai'), 350),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Nasional DIKTI'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Finalis'), 150),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Nasional DIKTI'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Peserta'), 25);

-- Nasional non-DIKTI
INSERT INTO AcuanPoin (id_tingkat, id_posisi, poin)
VALUES
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Nasional non-DIKTI'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Juara 1/2/3'), 350),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Nasional non-DIKTI'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Finalis'), 100),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Nasional non-DIKTI'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Peserta'), 20);

-- Regional
INSERT INTO AcuanPoin (id_tingkat, id_posisi, poin)
VALUES
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Regional'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Juara 1/2/3'), 200),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Regional'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Finalis'), 80),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Regional'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Peserta'), 15);

-- Universitas
INSERT INTO AcuanPoin (id_tingkat, id_posisi, poin)
VALUES
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Universitas'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Juara 1/2/3'), 50),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Universitas'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Finalis'), 25),
    ((SELECT id_tingkat FROM tingkatPrestasi WHERE nama_tingkat = 'Universitas'), (SELECT id_posisi FROM PosisiPrestasi WHERE nama_posisi = 'Peserta'), 10);