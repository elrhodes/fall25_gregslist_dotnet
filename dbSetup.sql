-- TODO don't forget to create your accounts table today!
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
) DEFAULT charset utf8mb4 COMMENT '';

-- NOTE make sure your id column is the *FIRST* column declared in this table
CREATE TABLE cars (
    id INT PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    make VARCHAR(50) NOT NULL,
    model VARCHAR(100) NOT NULL,
    `year` SMALLINT UNSIGNED NOT NULL,
    price MEDIUMINT UNSIGNED NOT NULL,
    img_url VARCHAR(500) NOT NULL,
    description VARCHAR(500),
    engine_type ENUM(
        'V6',
        'V8',
        'V10',
        '4-cylinder',
        'unknown'
    ) NOT NULL,
    color VARCHAR(50) NOT NULL,
    mileage MEDIUMINT NOT NULL,
    has_clean_title BOOLEAN NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
);

CREATE TABLE houses (
    id INT PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    sqft INT UNSIGNED NOT NULL,
    bedrooms INT UNSIGNED NOT NULL,
    bathrooms INT UNSIGNED NOT NULL,
    img_url VARCHAR(500) NOT NULL,
    description VARCHAR(500),
    price INT UNSIGNED NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE cars;

INSERT INTO
    houses (
        sqft,
        bedrooms,
        bathrooms,
        img_url,
        description,
        price,
        creator_id
    )
VALUES (
        1500,
        3,
        2,
        'https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8aG91c2V8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500',
        'Beautiful family home with spacious backyard.',
        300000,
        '68c3475a4b1978145b8fda91'
    ),
    (
        900,
        2,
        1,
        'https://images.unsplash.com/photo-1572120360610-d971b9bfa7c5?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y29uZG98ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500',
        'Cozy condo in the heart of the city.',
        200000,
        '68d309a495e19dc0812ef0cb'
    );

SELECT * FROM houses;

INSERT INTO
    cars (
        make,
        model,
        `year`,
        price,
        img_url,
        description,
        engine_type,
        color,
        mileage,
        has_clean_title,
        creator_id
    )
VALUES (
        'chevy',
        'cobalt',
        2008,
        1000,
        'https://images.unsplash.com/photo-1654851783043-c19620497da5?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8Y2hldnklMjBjb2JhbHR8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500',
        'most iconic car ever, no lowballs I KNOW WHAT I HAVE',
        '4-cylinder',
        'blue',
        1000000,
        FALSE,
        '670ff93326693293c631476f'
    ),
    (
        'mazda',
        'miata',
        1996,
        8000,
        'https://images.unsplash.com/photo-1552615526-40e47a79f9d7?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bWlhdGF8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500',
        NULL,
        '4-cylinder',
        'gray',
        80000,
        TRUE,
        '65f87bc1e02f1ee243874743'
    );

-- NOTE because of the ON DELETE CASCADE, if someone's account gets deleted it also deletes all of their cars
DELETE FROM accounts WHERE id = '670ff93326693293c631476f';

SELECT * FROM cars;

SELECT cars.*, accounts.*
FROM cars
    INNER JOIN accounts ON cars.creator_id = accounts.id
ORDER BY cars.id;

SELECT cars.*, accounts.*
FROM cars
    JOIN accounts ON accounts.id = cars.creator_id
WHERE
    cars.id = 1;

SELECT * FROM accounts;