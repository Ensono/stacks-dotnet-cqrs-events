
resource "random_string" "eh_storage" {
    count = "${var.app_bus_type == "eventhub" ? 1 : 0}"
    length  = 16
    upper   = false
    special = false
}

resource "azurerm_eventhub_namespace" "eh_ns" {
    count = "${var.app_bus_type == "eventhub" ? 1 : 0}"
    name = "${var.eh_name}-${random_string.seed.result}"
    resource_group_name = var.resource_group_name
    location            = var.resource_group_location
    sku                 = var.eventhub_sku
    capacity = var.eh_capacity   
}

resource "azurerm_eventhub" "eh" {
    count = "${var.app_bus_type == "eventhub" ? 1 : 0}"
    name = var.eh_name
    namespace_name = azurerm_eventhub_namespace.eh_ns.name
    resource_group_name = var.resource_group_name
    partition_count = var.eh_partition_count
    message_retention = var.retention
    depends_on = [
        azurerm_storage_container.eh_storage_container
    ]

    destination {
        name = "EventHubArchive.AzureBlockBlob"
        blob_container_name = azurerm_storage_container.eh_storage_container.name
        storage_account_id = azurerm_storage_account.eh_storage.id
    }
}

