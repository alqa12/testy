CREATE PROCEDURE GetTaskStatsByMonth
    AS
BEGIN
SELECT
    AssigneeId,
    COUNT(Id) AS TaskCount,
    DATEPART(MONTH, CreatedDate) AS Month
FROM
    Tasks
GROUP BY
    AssigneeId, DATEPART(MONTH, CreatedDate)
END