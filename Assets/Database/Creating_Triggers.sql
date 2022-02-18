CREATE TRIGGER IF NOT EXISTS newUser AFTER INSERT ON Users
BEGIN
INSERT INTO ProgressData(level_id, user_id, easy_star, easy_code, medium_star, medium_code, hard_star, hard_code)
SELECT level_id, NEW.user_id, 0, "", 0, "", 0, "" FROM Levels;
END

CREATE TRIGGER IF NOT EXISTS deleteUser BEFORE DELETE ON Users
BEGIN
DELETE FROM ProgressData WHERE ProgressData.user_id == OLD.user_id;
END

DROP TRIGGER newUser;

DROP TRIGGER deleteUser;

CREATE TRIGGER IF NOT EXISTS newLevel AFTER INSERT ON Levels
BEGIN
INSERT INTO ProgressData(level_id, user_id, easy_star, easy_code, medium_star, medium_code, hard_star, hard_code)
SELECT NEW.level_id, user_id, 0, "", 0, "", 0, "" FROM Users;
END

CREATE TRIGGER IF NOT EXISTS deleteLevel BEFORE DELETE ON Levels
BEGIN
DELETE FROM ProgressData WHERE ProgressData.level_id == OLD.level_id;
END

DROP TRIGGER newLevel;

DROP TRIGGER deleteLevel;