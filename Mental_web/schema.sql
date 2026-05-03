-- Mental Health System Database Schema
-- Target: MySQL

CREATE DATABASE IF NOT EXISTS `mental_health` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `mental_health`;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for admins
-- ----------------------------
DROP TABLE IF EXISTS `admins`;
CREATE TABLE `admins` (
  `admin_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `role` varchar(50) DEFAULT 'Admin',
  PRIMARY KEY (`admin_id`),
  UNIQUE KEY `idx_username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for students
-- ----------------------------
DROP TABLE IF EXISTS `students`;
CREATE TABLE `students` (
  `student_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `course` varchar(100) DEFAULT NULL,
  `year_level` int DEFAULT NULL,
  `email` varchar(150) NOT NULL,
  `contact_number` varchar(20) DEFAULT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`student_id`),
  UNIQUE KEY `idx_email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for counselors
-- ----------------------------
DROP TABLE IF EXISTS `counselors`;
CREATE TABLE `counselors` (
  `counselor_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(200) NOT NULL,
  `specialization` varchar(150) DEFAULT NULL,
  `email` varchar(150) NOT NULL,
  `contact_number` varchar(20) DEFAULT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`counselor_id`),
  UNIQUE KEY `idx_email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for appointments
-- ----------------------------
DROP TABLE IF EXISTS `appointments`;
CREATE TABLE `appointments` (
  `appointment_id` int NOT NULL AUTO_INCREMENT,
  `student_id` int NOT NULL,
  `counselor_id` int NOT NULL,
  `date` date NOT NULL,
  `time` time NOT NULL,
  `status` varchar(50) DEFAULT 'Pending', -- Pending, Approved, Completed, Cancelled
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`appointment_id`),
  KEY `fk_appointment_student` (`student_id`),
  KEY `fk_appointment_counselor` (`counselor_id`),
  CONSTRAINT `fk_appointment_counselor` FOREIGN KEY (`counselor_id`) REFERENCES `counselors` (`counselor_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_appointment_student` FOREIGN KEY (`student_id`) REFERENCES `students` (`student_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for schedules
-- ----------------------------
DROP TABLE IF EXISTS `schedules`;
CREATE TABLE `schedules` (
  `schedule_id` int NOT NULL AUTO_INCREMENT,
  `counselor_id` int NOT NULL,
  `available_date` date NOT NULL,
  `available_time` time NOT NULL,
  PRIMARY KEY (`schedule_id`),
  KEY `fk_schedule_counselor` (`counselor_id`),
  CONSTRAINT `fk_schedule_counselor` FOREIGN KEY (`counselor_id`) REFERENCES `counselors` (`counselor_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for self_assessments
-- ----------------------------
DROP TABLE IF EXISTS `self_assessments`;
CREATE TABLE `self_assessments` (
  `assessment_id` int NOT NULL AUTO_INCREMENT,
  `student_id` int NOT NULL,
  `score` int DEFAULT NULL,
  `result` text,
  `date_taken` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`assessment_id`),
  KEY `fk_assessment_student` (`student_id`),
  CONSTRAINT `fk_assessment_student` FOREIGN KEY (`student_id`) REFERENCES `students` (`student_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for feedbacks
-- ----------------------------
DROP TABLE IF EXISTS `feedbacks`;
CREATE TABLE `feedbacks` (
  `feedback_id` int NOT NULL AUTO_INCREMENT,
  `student_id` int NOT NULL,
  `appointment_id` int DEFAULT NULL,
  `message` text,
  `rating` int DEFAULT NULL,
  `date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`feedback_id`),
  KEY `fk_feedback_student` (`student_id`),
  KEY `fk_feedback_appointment` (`appointment_id`),
  CONSTRAINT `fk_feedback_appointment` FOREIGN KEY (`appointment_id`) REFERENCES `appointments` (`appointment_id`) ON DELETE SET NULL,
  CONSTRAINT `fk_feedback_student` FOREIGN KEY (`student_id`) REFERENCES `students` (`student_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for notifications
-- ----------------------------
DROP TABLE IF EXISTS `notifications`;
CREATE TABLE `notifications` (
  `notification_id` int NOT NULL AUTO_INCREMENT,
  `student_id` int NOT NULL,
  `message` text NOT NULL,
  `status` varchar(20) DEFAULT 'unread', -- unread, read
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`notification_id`),
  KEY `fk_notification_student` (`student_id`),
  CONSTRAINT `fk_notification_student` FOREIGN KEY (`student_id`) REFERENCES `students` (`student_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for assessment_questions
-- ----------------------------
DROP TABLE IF EXISTS `assessment_questions`;
CREATE TABLE `assessment_questions` (
  `question_id` int NOT NULL AUTO_INCREMENT,
  `question_text` text NOT NULL,
  `question_type` varchar(50) DEFAULT 'Scale', -- Scale, MultipleChoice, Text
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`question_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for resources
-- ----------------------------
DROP TABLE IF EXISTS `resources`;
CREATE TABLE `resources` (
  `resource_id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  `content_type` varchar(50) DEFAULT 'Article', -- Article, Guide, Video
  `content_body` text NOT NULL,
  `author_id` int DEFAULT NULL, -- Admin who created it
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`resource_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Table structure for activity_logs
-- ----------------------------
DROP TABLE IF EXISTS `activity_logs`;
CREATE TABLE `activity_logs` (
  `log_id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `role` varchar(50) NOT NULL,
  `action` varchar(255) NOT NULL,
  `timestamp` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

SET FOREIGN_KEY_CHECKS = 1;

-- ----------------------------
-- Insert Sample Data
-- ----------------------------
-- All sample users use the password: password123

INSERT INTO `admins` (`username`, `password_hash`, `role`) VALUES
('admin', '75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=', 'Admin');

INSERT INTO `counselors` (`name`, `specialization`, `email`, `contact_number`, `password_hash`) VALUES
('Dr. Sarah Jenkins', 'Cognitive Behavioral Therapy', 'counselor1@university.edu', '555-0101', '75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8='),
('Dr. Marcus Cole', 'Stress Management', 'counselor2@university.edu', '555-0102', '75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=');

INSERT INTO `students` (`first_name`, `last_name`, `course`, `year_level`, `email`, `contact_number`, `password_hash`) VALUES
('John', 'Doe', 'Computer Science', 2, 'student1@student.edu', '555-1234', '75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8='),
('Jane', 'Smith', 'Psychology', 3, 'student2@student.edu', '555-5678', '75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=');

INSERT INTO `appointments` (`student_id`, `counselor_id`, `date`, `time`, `status`) VALUES
(1, 1, DATE_ADD(CURDATE(), INTERVAL 2 DAY), '10:00:00', 'Approved'),
(2, 2, DATE_ADD(CURDATE(), INTERVAL 3 DAY), '14:30:00', 'Pending');

INSERT INTO `schedules` (`counselor_id`, `available_date`, `available_time`) VALUES
(1, DATE_ADD(CURDATE(), INTERVAL 2 DAY), '10:00:00'),
(1, DATE_ADD(CURDATE(), INTERVAL 2 DAY), '11:00:00'),
(2, DATE_ADD(CURDATE(), INTERVAL 3 DAY), '14:30:00');

INSERT INTO `notifications` (`student_id`, `message`, `status`) VALUES
(1, 'Your appointment with Dr. Sarah Jenkins is confirmed.', 'unread');
