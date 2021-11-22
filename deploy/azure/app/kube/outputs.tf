output "cosmosdb_database_name" {
  description = "CosmosDB Database name"
  value       = module.app.cosmosdb_database_name
}

output "cosmosdb_account_name" {
  description = "CosmosDB account name"
  value       = module.app.cosmosdb_account_name
}

output "cosmosdb_endpoint" {
  description = "Endpoint for accessing the DB CRUD"
  value       = module.app.cosmosdb_endpoint
}

output "cosmosdb_primary_master_key" {
  description = "Primary Key for accessing the DB CRUD, should only be used in applications running outside of AzureCloud"
  sensitive   = true
  value       = module.app.cosmosdb_primary_master_key
}

output "redis_cache_key" {
  description = "Primary Key for accessing the RedisCache, should only be used in applications running outside of AzureCloud"
  sensitive   = true
  value       = module.app.redis_cache_key
}

output "redis_cache_hostname" {
  description = "Primary Hostname endpoint for Redis Cache"
  value       = module.app.redis_cache_hostname
}

output "resource_group" {
  description = "Resource group name for the app"
  value       = module.app.resource_group
}

output "dns_name" {
  description = "DNS Name if created"
  value       = module.app.dns_name
}


output "servicebus_namespace" {
  description = "Service bus namespace"
  value       = module.servicebus[0].servicebus_namespace
}

output "servicebus_topic_name" {
  description = "Name of the topic"
  value       = module.servicebus[0].servicebus_topic_name
}

output "servicebus_subscription_name" {
  description = "Servicebus Subscription name"
  value       = module.servicebus[0].servicebus_subscription_name
}

output "servicebus_connectionstring" {
  value = module.servicebus[0].servicebus_connectionstring
}

output "servicebus_subscription_filtered_name" {
  description = "Servicebus Subscription filtered name"
  value       = module.servicebus[0].servicebus_subscription_filtered_name
}

output "servicebus_subscription_id" {
  description = "Servicebus Subscription ID"
  value       = module.servicebus[0].servicebus_subscription_id
}

output "servicebus_subscription_filtered_id" {
  description = "Servicebus Subscription filtered ID"
  value       = module.servicebus[0].servicebus_subscription_filtered_id
}

output "function_publisher_id" {
  value = module.servicebus[0].function_publisher_id
}

output "function_listener_id" {
  value = module.servicebus[0].function_listener_id
}

output "function_publisher_name" {
  value = module.servicebus[0].function_publisher_name
}

output "function_listener_name" {
  value = module.servicebus[0].function_listener_name
}

output "eventhub_connectionstring" {
  value = module.eventhub[0].eventhub_connectionstring
}

output "eventhub_name" {
  value = module.eventhub[0].eventhub_name
}

output "eventhub_sa_connectionstring" {
  value = module.eventhub[0].eventhub_sa_connectionstring
}

output "eventhub_sa_container" {
  value = module.eventhub[0].eventhub_sa_container
}