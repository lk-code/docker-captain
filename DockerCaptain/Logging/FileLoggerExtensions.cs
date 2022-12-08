﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace DockerCaptain.Logging;

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFileLogger(
        this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions<FileLoggerConfiguration, FileLoggerProvider>(builder.Services);

        return builder;
    }

    public static ILoggingBuilder AddFileLogger(
        this ILoggingBuilder builder,
        Action<FileLoggerConfiguration> configure)
    {
        builder.AddFileLogger();
        builder.Services.Configure(configure);

        return builder;
    }
}