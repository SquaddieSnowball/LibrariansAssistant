﻿using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Entities;

namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers;

public interface IObjectRelationalMapper
{
    int ExecuteNonQuery(string query, QueryExecutionSettings? executionSettings);

    IEnumerable<int> ExecuteNonQueries(IEnumerable<string> queries, QueryExecutionSettings? executionSettings);

    IEnumerable<T> ExecuteQuery<T>(string query, QueryExecutionSettings? executionSettings);

    T ExecuteScalar<T>(string query, QueryExecutionSettings? executionSettings);

    IObjectRelationalMapper AddParameter<T>(string name, T value);

    void RegisterDependency(Type abstractType, Type concreteType);
}