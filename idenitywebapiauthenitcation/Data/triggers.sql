use db_eccomerce
go

CREATE TRIGGER trg_Products_Insert
ON Products
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ProductId INT;
    DECLARE @ProductDescription NVARCHAR(MAX);
    DECLARE @ProductCost DECIMAL(18, 2);
    DECLARE @ProductExistence INT;

    -- Obtener los datos del nuevo producto insertado
    SELECT @ProductId = Id,
           @ProductDescription = Description,
           @ProductCost = Cost,
           @ProductExistence = Existence
    FROM inserted;


	-- Insertar un nuevo lote en la tabla de lotes
    DECLARE @BatchId INT;

    INSERT INTO Batches (InitialQuantity, RemainingQuantity, EntryDate, Cost, ProductId)
    VALUES (@ProductExistence, @ProductExistence, GETDATE(), @ProductCost, @ProductId);

    SET @BatchId = SCOPE_IDENTITY(); -- Obtener el ID del lote recién insertado


     -- Insertar una nueva entrada en la tabla de entradas
    DECLARE @EntryId INT;
    DECLARE @EntryTotal DECIMAL(18, 2);

    SET @EntryTotal = @ProductCost * @ProductExistence;

    INSERT INTO Entries (Date, Total, StateId)
    VALUES (GETDATE(), @EntryTotal, 1); -- 1 == activo

    SET @EntryId = SCOPE_IDENTITY(); -- Obtener el ID de la entrada recién insertada

    -- Insertar una nueva fila en la tabla de detalles de entrada
    INSERT INTO EntryDetails (UnitCost, Amount, Description, EntryTypeId, EntryId, ProductId, BatchId)
    VALUES (@ProductCost, @ProductExistence, @ProductDescription, 1, @EntryId, @ProductId, @BatchId); -- 1 == compra
END;
