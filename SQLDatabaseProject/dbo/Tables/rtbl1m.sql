﻿CREATE TABLE [dbo].[rtbl1m] (
    [x] FLOAT (53) NOT NULL,
    [y] FLOAT (53) NOT NULL,
    [z] FLOAT (53) NOT NULL,
    [t] CHAR (33)  NOT NULL,
    PRIMARY KEY NONCLUSTERED HASH ([x], [y], [z]) WITH (BUCKET_COUNT = 2097152),
    INDEX [ix_rtbl1m_t] NONCLUSTERED HASH ([t]) WITH (BUCKET_COUNT = 1048576),
    INDEX [ix_rtbl1m_z] NONCLUSTERED HASH ([z]) WITH (BUCKET_COUNT = 524288),
    INDEX [ix_rtbl1m_y] NONCLUSTERED HASH ([y]) WITH (BUCKET_COUNT = 524288),
    INDEX [ix_rtbl1m_x] NONCLUSTERED HASH ([x]) WITH (BUCKET_COUNT = 524288)
)
WITH (MEMORY_OPTIMIZED = ON);

