output "servicebus_namespace" {
    description = "Service bus namespace"
    value = azurerm_servicebus_namespace.sb.name
}

output "servicebus_subscription_name" {
    description = "Servicebus Subscription name"
    value = azurerm_servicebus_subscription.sb_sub_1.name
}

output "servicebus_subscription_filtered_name" {
    description = "Servicebus Subscription filtered name"
    value = azurerm_servicebus_subscription.sb_sub_2.name
}

output "event_function_names" {
    description = "Functions used for events"
    value = azurerm_function_app.function.*.name
}