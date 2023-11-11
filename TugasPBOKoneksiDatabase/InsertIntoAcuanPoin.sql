INSERT INTO AcuanPoin (id_tingkat, id_posisi, poin)
SELECT PP.id_tingkat, PP.id_posisi, 
    CASE
        WHEN PP.nama_posisi LIKE 'Juara%' THEN 500
        WHEN PP.nama_posisi = 'Terpilih / Didanai' THEN 350
        WHEN PP.nama_posisi = 'Finalis' THEN 200
        WHEN PP.nama_posisi = 'Peserta' THEN 50
        ELSE NULL  -- Adjust this based on your specific criteria
    END AS poin
FROM PosisiPrestasi PP
WHERE PP.id_tingkat IS NOT NULL;
