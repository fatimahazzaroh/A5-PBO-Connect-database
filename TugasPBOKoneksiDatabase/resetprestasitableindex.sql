-- Mengatur ulang nilai identitas kolom ID pada tabel Prestasi menjadi 1
DBCC CHECKIDENT ('Prestasi', RESEED, 0);