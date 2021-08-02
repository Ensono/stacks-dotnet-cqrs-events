
# Create a random string to be used for the 

resource "azurerm_servicebus_namespace" "sb" {
    name = "servicebus-${random_string.seed.result}"
    resource_group_name = azurerm_resource_group.rg.name
    location = azurerm_resource_group.rg.location
    sku = var.servicebus_sku
}

resource "azurerm_servicebus_topic" "sb_topic" {
    name = vars.topic_name
    resource_group_name = azurerm_resource_group.rg.name
    namespace_name = azurerm_servicebus_namespace.sb.name
}

resource "azurerm_servicebus_subscription" "sb_sub_1" {
    name = vars.subscription_name
    resource_group_name = azurerm_resource_group.rg.name
    namespace_name = azurerm_servicebus_namespace.sb.name
    topic_name = azurerm_servicebus_topic.sb_topic.name
}

resource "azurerm_servicebus_subscription" "sb_sub_2" {
    name = vars.subscription_name_filtered
    resource_group_name = azurerm_resource_group.rg.name
    namespace_name = azurerm_servicebus_namespace.sb.name
    topic_name = azurerm_servicebus_topic.sb_topic.name

    filter_type = "SqlFilter"
    aql_filter = "enclosedmessagetype like '%MenuCreatedEvent%'"
}